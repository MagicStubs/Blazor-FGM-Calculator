using System.Text;
using CsvHelper;
using CsvHelper.TypeConversion;
using FGM.Data.Exceptions;
using FGM.DataLayer.Interfaces;
using FGM.DataLayer.Services;
using Microsoft.AspNetCore.Components.Forms;
using Moq;

namespace FGM.DataLayer.Tests;

public class Tests
{
    private ICsvDeserializerService<Survey> _service;

    [SetUp]
    public void Setup()
    {
        _service = new SurveyCsvDeserializerService();
    }

    [Test]
    public void DeserializeCsvFile_FileIsNotCsv_IncorrectFileFormatExecptionThrown()
    {
        // arrange
        var fileName = "PlainText.txt";
        var mockBrowserFile = new Mock<IBrowserFile>();
        mockBrowserFile.Setup(f => f.Name)
            .Returns(fileName);

        var expectedErrorMessage = $"The file type '.txt' is not supported. Only CSV files are allowed.";

        // act and assert
        Assert.Multiple(() =>
        {
            var exception =
                Assert.Throws<InvalidFileTypeException>(() =>
                    _service.DeserializeCsvFile(mockBrowserFile.Object));
            Assert.That(exception?.Message, Is.EqualTo(expectedErrorMessage));
        });
    }

    [Test]
    public void DeserializeCsvFile_IncorrectCsvFormatWithIncorrectHeaderRow_HeaderValidationErrorThrown()
    {
        // arrange
        var stream = new FileStream("../../../TestFiles/IncorrectFormatWithHeader.csv", FileMode.Open, FileAccess.Read);
        var mockBrowserFile = new Mock<IBrowserFile>();
        mockBrowserFile.Setup(f => f.OpenReadStream(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .Returns(stream);
        mockBrowserFile.Setup(f => f.Name)
            .Returns("IncorrectFormatWithHeader.csv");
        
        // act and assert
        Assert.Multiple(() =>
        {
            Assert.Throws<HeaderValidationException>(
                () => _service.DeserializeCsvFile(mockBrowserFile.Object));
        });
    }


    [Test] 
    public void DeserializeCsvFile_CsvWithIncorrectlyFormatedData_TypeConverterException()
    {
        // arrange
        var stream = new FileStream("../../../TestFiles/IncorrectlyFormatedData.csv", FileMode.Open, FileAccess.Read);
        var mockBrowserFile = new Mock<IBrowserFile>();
        mockBrowserFile.Setup(f => f.OpenReadStream(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .Returns(stream);
        mockBrowserFile.Setup(f => f.Name)
            .Returns("IncorrectlyFormatedData.csv");
        
        // act and assert
        Assert.Multiple(() =>
        {
            Assert.Throws<TypeConverterException>(
                () => _service.DeserializeCsvFile(mockBrowserFile.Object));
        });
    }
    
    [Test] 
    public void DeserializeCsvFile_ValidCsvWithHeader_CsvDeserialized()
    {
        // arrange
        var stream = new FileStream("../../../TestFiles/ValidDataWithHeader.csv", FileMode.Open, FileAccess.Read);
        var mockBrowserFile = new Mock<IBrowserFile>();
        mockBrowserFile.Setup(f => f.OpenReadStream(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .Returns(stream);
        mockBrowserFile.Setup(f => f.Name)
            .Returns("ValidDataWithHeader.csv");
        
        // act 
        var result = _service.DeserializeCsvFile(mockBrowserFile.Object);
        
        // assert
        Assert.That(result.Count, Is.EqualTo(2));
    }
}