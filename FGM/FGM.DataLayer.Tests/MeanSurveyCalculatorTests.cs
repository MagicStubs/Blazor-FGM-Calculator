using FGM.DataLayer.Calculators;
using FGM.DataLayer.Interfaces;

namespace FGM.DataLayer.Tests;

public class MeanSurveyCalculatorTests
{
    private IMeanSurveyCalculator _meanSurveyCalculator;
    
    [SetUp]
    public void SetUp()
    {
        _meanSurveyCalculator = new MeanSurveyCalculator();
    }

    [Test]
    public void CalculatedMeanSurvey_OneSurveyGiven_ExpectedSuveyOutPut()
    {
        // arrange 
        const string name = "Test Survey";
        const int yearOfSurvey = 2020;
        const string countryCode = "Test 123";
        const string countryName = "Test Country";

        var surveys = CreateTestSurvey(1, name, countryCode, countryName, yearOfSurvey);

        var expectedName = "Mean Survey";
        var expectedCountryCode = countryCode;
        var expectedCountryName = countryName;
        var expectedYearOfSurvey = yearOfSurvey;
        
        // act
        var result = _meanSurveyCalculator.CalculateMeanSurvey(surveys);
        
        // assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo(expectedName));
            Assert.That(result.CountryCode, Is.EqualTo(expectedCountryCode));
            Assert.That(result.CountryName, Is.EqualTo(expectedCountryName));
            Assert.That(result.YearOfSurvey, Is.EqualTo(expectedYearOfSurvey));
            Assert.That(result.ZeroToFour, Is.EqualTo(surveys[0].ZeroToFour));
            Assert.That(result.FiveToNine, Is.EqualTo(surveys[0].FiveToNine));
            Assert.That(result.TenToForteen, Is.EqualTo(surveys[0].TenToForteen));
            Assert.That(result.FifteenToNineteen, Is.EqualTo(surveys[0].FifteenToNineteen));
            Assert.That(result.TwentyToTwentyfour, Is.EqualTo(surveys[0].TwentyToTwentyfour));
            Assert.That(result.TwentyfiveToTwentynine, Is.EqualTo(surveys[0].TwentyfiveToTwentynine));
            Assert.That(result.ThirtyToThirtyfour, Is.EqualTo(surveys[0].ThirtyToThirtyfour));
            Assert.That(result.ThirtyfiveToThitrynine, Is.EqualTo(surveys[0].ThirtyfiveToThitrynine));
            Assert.That(result.FourtyToFourtyfour, Is.EqualTo(surveys[0].FourtyToFourtyfour));
            Assert.That(result.FourtyfiveToFourtynine, Is.EqualTo(surveys[0].FourtyfiveToFourtynine));
            Assert.That(result.FifteyToFifteyfour, Is.EqualTo(surveys[0].FifteyToFifteyfour));
            Assert.That(result.FifityfiveToFiftynine, Is.EqualTo(surveys[0].FifityfiveToFiftynine));
            Assert.That(result.SixtyToSixtyfour, Is.EqualTo(surveys[0].SixtyToSixtyfour));
            Assert.That(result.SixtyfiveToSixtynine, Is.EqualTo(surveys[0].SixtyfiveToSixtynine));
            Assert.That(result.SeventyToSeventyfour, Is.EqualTo(surveys[0].SeventyToSeventyfour));
            Assert.That(result.SeventyfiveToSeventynine, Is.EqualTo(surveys[0].SeventyfiveToSeventynine));
            Assert.That(result.EightyPlus, Is.EqualTo(surveys[0].EightyPlus));
        });
    }
    
    [Test]
    public void CalculatedMeanSurvey_MultipleSurveyGiven_OutPutAgeDataIsMeanOfAllInputs()
    {
        // arrange 
        const string name = "Test Survey";
        const int yearOfSurvey = 2020;
        const string countryCode = "Test 123";
        const string countryName = "Test Country";

        var surveys = CreateTestSurvey(3, name, countryCode, countryName, yearOfSurvey);

        var expectedName = "Mean Survey";
        var expectedCountryCode = countryCode;
        var expectedCountryName = countryName;
        var expectedYearOfSurvey = yearOfSurvey;

        var expectedAgeData = (10 + 20 + 30) / 3;
        // act
        var result = _meanSurveyCalculator.CalculateMeanSurvey(surveys);
        
        // assert
        Assert.Multiple(() =>
        {
            Assert.That(result.Name, Is.EqualTo(expectedName));
            Assert.That(result.CountryCode, Is.EqualTo(expectedCountryCode));
            Assert.That(result.CountryName, Is.EqualTo(expectedCountryName));
            Assert.That(result.YearOfSurvey, Is.EqualTo(expectedYearOfSurvey));
            Assert.That(result.ZeroToFour, Is.EqualTo(expectedAgeData));
            Assert.That(result.FiveToNine, Is.EqualTo(expectedAgeData));
            Assert.That(result.TenToForteen, Is.EqualTo(expectedAgeData));
            Assert.That(result.FifteenToNineteen, Is.EqualTo(expectedAgeData));
            Assert.That(result.TwentyToTwentyfour, Is.EqualTo(expectedAgeData));
            Assert.That(result.TwentyfiveToTwentynine, Is.EqualTo(expectedAgeData));
            Assert.That(result.ThirtyToThirtyfour, Is.EqualTo(expectedAgeData));
            Assert.That(result.ThirtyfiveToThitrynine, Is.EqualTo(expectedAgeData));
            Assert.That(result.FourtyToFourtyfour, Is.EqualTo(expectedAgeData));
            Assert.That(result.FourtyfiveToFourtynine, Is.EqualTo(expectedAgeData));
            Assert.That(result.FifteyToFifteyfour, Is.EqualTo(expectedAgeData));
            Assert.That(result.FifityfiveToFiftynine, Is.EqualTo(expectedAgeData));
            Assert.That(result.SixtyToSixtyfour, Is.EqualTo(expectedAgeData));
            Assert.That(result.SixtyfiveToSixtynine, Is.EqualTo(expectedAgeData));
            Assert.That(result.SeventyToSeventyfour, Is.EqualTo(expectedAgeData));
            Assert.That(result.SeventyfiveToSeventynine, Is.EqualTo(expectedAgeData));
            Assert.That(result.EightyPlus, Is.EqualTo(expectedAgeData));
        });
    }

    private List<CalculatedSurvey> CreateTestSurvey(int numberOfSurveys, string name, string countryCode, string countryName, int yearOfSurvey)
    {
        List<CalculatedSurvey> surveys = [];
        for (int i = 1; i <= numberOfSurveys; i++)
        {
            var survey = new CalculatedSurvey()
            {
                Name = name,
                YearOfSurvey = yearOfSurvey,
                CountryCode = countryCode,
                CountryName = countryName,
                ZeroToFour = i * 10,
                FiveToNine = i * 10,
                TenToForteen = i * 10,
                FifteenToNineteen = i * 10,
                TwentyToTwentyfour = i * 10,
                TwentyfiveToTwentynine = i * 10,
                ThirtyToThirtyfour = i * 10,
                ThirtyfiveToThitrynine = i * 10,
                FourtyToFourtyfour = i * 10,
                FourtyfiveToFourtynine = i * 10,
                FifteyToFifteyfour = i * 10,
                FifityfiveToFiftynine = i * 10,
                SixtyToSixtyfour = i * 10,
                SixtyfiveToSixtynine = i * 10,
                SeventyToSeventyfour = i * 10,
                SeventyfiveToSeventynine = i * 10,
                EightyPlus = i * 10
            };
            surveys.Add(survey);
        }

        return surveys;
    }
}