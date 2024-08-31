using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Forms;
using CsvHelper;
using FGM.Data;
using FGM.Data.Exceptions;
using FGM.DataLayer;
using FGM.DataLayer.Services;
using Microsoft.Data.Sqlite;
using Moq;

[TestFixture]
public class SurveyServiceTests
{
    private FgmDbContext _context;
    private SurveyService _surveyService;
    private SurveyCsvDeserializerService _csvDeserializerService;

    [SetUp]
    public void Setup()
    {
        // Set up the SQLite in-memory database
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        var options = new DbContextOptionsBuilder<FgmDbContext>()
            .UseSqlite(connection)
            .Options;

        _context = new FgmDbContext(options);
        _context.Database.EnsureCreated();

        // Create an instance of SurveyService for testing
        _csvDeserializerService = new SurveyCsvDeserializerService(); // Replace with your ICsvDeserializerService implementation
        _surveyService = new SurveyService(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public void ImportSurveys_ValidCSV_SurveysAddedToDatabase()
    {
        // Arrange
        var stream = new FileStream("../../../TestFiles/ValidDataWithHeader.csv", FileMode.Open, FileAccess.Read);
        var mockBrowserFile = new Mock<IBrowserFile>();
        mockBrowserFile.Setup(f => f.OpenReadStream(It.IsAny<long>(), It.IsAny<CancellationToken>())).Returns(stream);
        mockBrowserFile.Setup(f => f.Name)
            .Returns("name.csv");

        var surveys = _csvDeserializerService.DeserializeCsvFile(mockBrowserFile.Object);
        
        // Act
        var result = _surveyService.ImportSurveys(surveys).Result;
        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(result, Is.EqualTo(2)); // Assuming 2 surveys are added
            Assert.That(_context.Surveys.Count(), Is.EqualTo(2)); // Verify the surveys are added to the database
        });
    }
    
    
    
    [Test]
    public void GetSurveys_ReturnsAllSurveys()
    {
        // Arrange
        var surveys = new List<Survey>
        {
            new Survey { Name = "John Doe", YearOfSurvey = 2020, CountryName = "test", CountryCode = "t"},
            new Survey { Name = "Jane Smith", YearOfSurvey = 2021, CountryName = "test", CountryCode = "t" }
        };
        _context.Surveys.AddRange(surveys);
        _context.SaveChanges();

        // Act
        var result = _surveyService.GetSurveys().Result;

        // Assert
        Assert.That(result.Count, Is.EqualTo(2));
        CollectionAssert.AreEquivalent(surveys, result);
    }

    [Test]
    public void GetSurveys_ReturnsEmptyList_WhenNoSurveysExist()
    {
        // Act
        var result = _surveyService.GetSurveys().Result;

        // Assert
        Assert.IsEmpty(result);
    }
}
