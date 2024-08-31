using FGM.Data.Calculators;
using FGM.Data.Enums;
using FGM.Data.Factory;
using FGM.DataLayer.Calculators;
using FGM.DataLayer.Interfaces;
using FGM.DataLayer.Services;

namespace FGM.DataLayer.Tests;

[TestFixture]
public class FgmCaculatorTests
{
    private IFgmCalculator _calculator;

    [SetUp]
    public void SetUp()
    {
        IExtrapolationService service = new ExtrapolationService();
        var yongerExtrapolationCalculators =
            new Dictionary<YongerExtrapolationMethod, Func<IYongerExtrapolationCalculator>>()
            {
                [YongerExtrapolationMethod.FiveYear] = () => new FiveYearExtrapolationCalculator(service),
                [YongerExtrapolationMethod.TenYear] = () => new TenYearExtrapolationCalculator(service),
                [YongerExtrapolationMethod.FifteenYear] = () => new FifteenYearExtrapolationCalculator(service)
            };

        var olderExtrapolationCalculators =
            new Dictionary<OlderExtrapolationMethod, Func<IOlderExtrapolationCalculator>>()
            {
                [OlderExtrapolationMethod.Flatten] = () => new FlattenExtrapolationCalculator(service)
            };
        
        IYongerExtrapolationCalculatorFactorty yongerExtrapolationCalculatorFactorty = new YongerExtrapolationCalculatorFactory(yongerExtrapolationCalculators);
        IOlderExtrapolationCalculatorFactorty olderExtrapolationCalculatorFactorty = new OlderExtrapolationCalculatorFactory(olderExtrapolationCalculators);
        _calculator = new FgmCalculator(yongerExtrapolationCalculatorFactorty, olderExtrapolationCalculatorFactorty);
    }

    [TearDown]
    public void TearDown()
    {
    }


    [Test]
    public void CalculateTemp10to14_InputSurvey_TempCalculated()
    {
        // arrange
        var survey = new Survey()
        {
            CountryName = "Test",
            CountryCode = "Tes",
            YearOfSurvey = 2024,
            Name = "Test",
            FifteenToNineteen = 64.2m,
            TwentyToTwentyfour = 70.7m,
            TwentyfiveToTwentynine = 75.0m,
            ThirtyToThirtyfour = 73.7m,
            ThirtyfiveToThitrynine = 74.1m,
            FourtyToFourtyfour = 76.7m,
            FourtyfiveToFourtynine = 74.1m
        };

        // act
        var result = _calculator.CalculateTemp10To14(survey);

        Assert.That(result,
            Is.EqualTo(survey.FifteenToNineteen / survey.TwentyToTwentyfour * survey.FifteenToNineteen));
    }


    [Test]
    public void CalculateTemp50to54_InputSurvey_TempCalculated()
    {
        // arrange
        var survey = new Survey()
        {
            CountryName = "Test",
            CountryCode = "Tes",
            YearOfSurvey = 2024,
            Name = "Test",
            FifteenToNineteen = 64.2m,
            TwentyToTwentyfour = 70.7m,
            TwentyfiveToTwentynine = 75.0m,
            ThirtyToThirtyfour = 73.7m,
            ThirtyfiveToThitrynine = 74.1m,
            FourtyToFourtyfour = 76.7m,
            FourtyfiveToFourtynine = 74.1m
        };

        // act
        var result = _calculator.CalculateTemp50To54(survey);

        Assert.That(result,
            Is.EqualTo(survey.FourtyfiveToFourtynine / survey.FourtyToFourtyfour * survey.FourtyfiveToFourtynine));
    }

    [Test]
    public void CalculateDifference_TargetYearGreaterThanSurveyYear_ReturnsPositiveDifference()
    {
        // Arrange
        var targetYear = 2025;
        var surveyYear = 2020;
        var expectedResult = 5;

        // Act
        var result = _calculator.CalculateDifference(targetYear, surveyYear);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void CalculateDifference_TargetYearLessThanSurveyYear_ReturnsNegativeDifference()
    {
        // Arrange
        var targetYear = 2020;
        var surveyYear = 2025;
        var expectedResult = -5;

        // Act
        var result = _calculator.CalculateDifference(targetYear, surveyYear);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void CalculateDifference_TargetYearEqualToSurveyYear_ReturnsZero()
    {
        // Arrange
        var targetYear = 2025;
        var surveyYear = 2025;
        var expectedResult = 0;

        // Act
        var result = _calculator.CalculateDifference(targetYear, surveyYear);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void CalculateQuotientAndRemainder_DifferenceDivisibleBy5_ReturnsCorrectQuotientAndZeroRemainder()
    {
        // Arrange
        var difference = 25;
        var expectedQuotient = 5;
        var expectedRemainder = 0;

        // Act
        var result = _calculator.CalculateQuotientAndRemainder(difference, out var remainder);

        // Assert
        Assert.That(result, Is.EqualTo(expectedQuotient));
        Assert.That(remainder, Is.EqualTo(expectedRemainder));
    }

    [Test]
    public void CalculateQuotientAndRemainder_DifferenceNotDivisibleBy5_ReturnsCorrectQuotientAndRemainder()
    {
        // Arrange
        var difference = 23;
        var expectedQuotient = 4;
        var expectedRemainder = 3;

        // Act
        var result = _calculator.CalculateQuotientAndRemainder(difference, out var remainder);

        // Assert
        Assert.That(result, Is.EqualTo(expectedQuotient));
        Assert.That(remainder, Is.EqualTo(expectedRemainder));
    }

    [Test]
    public void CalculateShiftedAgeData_SurveyYearOlderThenTagetYear_ReturnsExpectedResult()
    {
        var survey = new Survey()
        {
            CountryName = "Burkina Faso",
            CountryCode = "BFA",
            YearOfSurvey = 1999,
            Name = "1999 DHS",
            FifteenToNineteen = 64.2m,
            TwentyToTwentyfour = 70.7m,
            TwentyfiveToTwentynine = 75.0m,
            ThirtyToThirtyfour = 73.7m,
            ThirtyfiveToThitrynine = 74.1m,
            FourtyToFourtyfour = 76.7m,
            FourtyfiveToFourtynine = 74.1m
        };

        var expectedResult = new decimal[]
        {
            0, 0, 0, 0, 63.01951909476661951909476662m, 69.4m, 74.14m, 73.96m, 74.02m, 76.18m, 74.62m, 0, 0, 0, 0, 0, 0
        };

        // Act
        var result = _calculator.CalculateShiftedAgeData(survey, 2005);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void CalculateShiftedAgeData_SurveyYearNewerThanTargetYear_ReturnsExpectedResult()
    {
        var survey = new Survey()
        {
            CountryName = "Burkina Faso",
            CountryCode = "BFA",
            YearOfSurvey = 2021,
            Name = "2021 DHS",
            FifteenToNineteen = 32.2m,
            TwentyToTwentyfour = 46.1m,
            TwentyfiveToTwentynine = 54.5m,
            ThirtyToThirtyfour = 64.1m,
            ThirtyfiveToThitrynine = 72.2m,
            FourtyToFourtyfour = 77.2m,
            FourtyfiveToFourtynine = 82.5m
        };

        var expectedResult = new decimal[]
        {
            34.98m, 47.78m, 56.42m, 65.72m, 73.2m, 78.26m, 83.63277202072538860103626943m, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };

        // Act
        var result = _calculator.CalculateShiftedAgeData(survey, 2005);

        // Assert
        Assert.That(result, Is.EqualTo(expectedResult));
    }
    
    [Test]
    public void CalculateYongerAgeGroups_FiveYearExtrapolationMethod_ExpectedResultReturned()
    {
        var survey = new Survey()
        {
            CountryName = "Burkina Faso",
            CountryCode = "BFA",
            YearOfSurvey = 1999,
            Name = "1999 DHS",
            FifteenToNineteen = 64.2m,
            TwentyToTwentyfour = 70.7m,
            TwentyfiveToTwentynine = 75.0m,
            ThirtyToThirtyfour = 73.7m,
            ThirtyfiveToThitrynine = 74.1m,
            FourtyToFourtyfour = 76.7m,
            FourtyfiveToFourtynine = 74.1m
        };

        var expectedResult = new decimal[]
        {
            42.848689300075150540422597532m, 
            47.186952235441014691711489803m,
            51.96444739946541649071654718m, 
            57.225645344894158035726789493m,
            63.01951909476661951909476662m,
            69.4m,
            74.14m,
            73.96m,
            74.02m,
            76.18m,
            74.62m,
            0, 0, 0, 0, 0, 0
        };
        
        var shiftedAgeData = _calculator.CalculateShiftedAgeData(survey, 2005);

        // Act
        var result = _calculator.CalculateYongerAgeGroups(shiftedAgeData, YongerExtrapolationMethod.FiveYear);
        
        //Assert
        
        Assert.That(result, Is.EqualTo(expectedResult));
    }
    
    [Test]
    public void CalculateYongerAgeGroups_TenYearExtrapolationMethod_ExpectedResultReturned()
    {
        var survey = new Survey()
        {
            CountryName = "Burkina Faso",
            CountryCode = "BFA",
            YearOfSurvey = 1999,
            Name = "1999 DHS",
            FifteenToNineteen = 64.2m,
            TwentyToTwentyfour = 70.7m,
            TwentyfiveToTwentynine = 75.0m,
            ThirtyToThirtyfour = 73.7m,
            ThirtyfiveToThitrynine = 74.1m,
            FourtyToFourtyfour = 76.7m,
            FourtyfiveToFourtynine = 74.1m
        };

        var expectedResult = new decimal[]
        {
            44.779383315282430934274956627m,
            48.842402405444431137681734199m,
            53.172564169657581995130481268m, 
            58.108065626161695922538177616m,
            63.01951909476661951909476662m,
            69.4m,
            74.14m,
            73.96m,
            74.02m,
            76.18m,
            74.62m,
            0, 0, 0, 0, 0, 0
        };
        
        var shiftedAgeData = _calculator.CalculateShiftedAgeData(survey, 2005);

        // Act
        var result = _calculator.CalculateYongerAgeGroups(shiftedAgeData, YongerExtrapolationMethod.TenYear);
        
        //Assert
        
        Assert.That(result, Is.EqualTo(expectedResult));
    }
    
    [Test]
    public void CalculateYongerAgeGroups_FifteenYearExtrapolationMethod_ExpectedResultReturned()
    {
        var survey = new Survey()
        {
            CountryName = "Burkina Faso",
            CountryCode = "BFA",
            YearOfSurvey = 1999,
            Name = "1999 DHS",
            FifteenToNineteen = 64.2m,
            TwentyToTwentyfour = 70.7m,
            TwentyfiveToTwentynine = 75.0m,
            ThirtyToThirtyfour = 73.7m,
            ThirtyfiveToThitrynine = 74.1m,
            FourtyToFourtyfour = 76.7m,
            FourtyfiveToFourtynine = 74.1m
        };

        var expectedResult = new decimal[]
        {
            48.443266589325673894758044584m,
            51.734394232927100753951152482m,
            55.670063491362918154600131058m, 
            59.796341324625181323271738010m,
            63.01951909476661951909476662m,
            69.4m,
            74.14m,
            73.96m,
            74.02m,
            76.18m,
            74.62m,
            0, 0, 0, 0, 0, 0
        };
        
        var shiftedAgeData = _calculator.CalculateShiftedAgeData(survey, 2005);

        // Act
        var result = _calculator.CalculateYongerAgeGroups(shiftedAgeData, YongerExtrapolationMethod.FifteenYear);
        
        //Assert
        
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void CalculateOlderAgeGroups_Flatten_OlderAgeGroupsSetToOrigonal45To49()
    {
        // arrange
        var survey = new Survey()
        {
            CountryName = "Burkina Faso",
            CountryCode = "BFA",
            YearOfSurvey = 1999,
            Name = "1999 DHS",
            FifteenToNineteen = 64.2m,
            TwentyToTwentyfour = 70.7m,
            TwentyfiveToTwentynine = 75.0m,
            ThirtyToThirtyfour = 73.7m,
            ThirtyfiveToThitrynine = 74.1m,
            FourtyToFourtyfour = 76.7m,
            FourtyfiveToFourtynine = 74.1m
        };

        var calculatedSurvey = new CalculatedSurvey()
        {
            OrigonalSurvey = survey,
            TempAgeData = new [] {0, 0, 0, 64.2m, 70.7m ,75.0m, 73.7m, 74.1m, 76.7m, 74.1m, 0, 0, 0, 0, 0, 0, 0}
        };


        var expected = new decimal[17] {0, 0, 0, 64.2m, 70.7m ,75.0m, 73.7m, 74.1m, 76.7m, 74.1m, 74.1m, 74.1m, 74.1m, 74.1m, 74.1m, 74.1m, 74.1m};
        // act
        var result = _calculator.CalculateOlderAgeGroups(calculatedSurvey, OlderExtrapolationMethod.Flatten);
        
        // Assert
        Assert.That(result, Is.EqualTo(expected));

    }

    [Test]
    public void RunFgmCalculations_CalculationsRun_ExpectedResultReturned()
    {
        var survey = new Survey()
        {
            CountryName = "Burkina Faso",
            CountryCode = "BFA",
            YearOfSurvey = 1999,
            Name = "1999 DHS",
            FifteenToNineteen = 64.2m,
            TwentyToTwentyfour = 70.7m,
            TwentyfiveToTwentynine = 75.0m,
            ThirtyToThirtyfour = 73.7m,
            ThirtyfiveToThitrynine = 74.1m,
            FourtyToFourtyfour = 76.7m,
            FourtyfiveToFourtynine = 74.1m
        };
        var targetYear = 2005;
        var expectedCalculatedSurvey = new CalculatedSurvey()
        {
            OrigonalSurvey = survey,
            YearOfSurvey = targetYear,
            Name = survey.Name,
            CountryCode = survey.CountryCode,
            CountryName = survey.CountryName,
            ZeroToFour = 42.848689300075150540422597532m,
            FiveToNine = 47.186952235441014691711489803m,
            TenToForteen = 51.96444739946541649071654718m,
            FifteenToNineteen =  57.225645344894158035726789493m,
            TwentyToTwentyfour = 63.01951909476661951909476662m,
            TwentyfiveToTwentynine = 69.4m,
            ThirtyToThirtyfour = 74.14m,
            ThirtyfiveToThitrynine = 73.96m,
            FourtyToFourtyfour = 74.02m,
            FourtyfiveToFourtynine = 76.18m,
            FifteyToFifteyfour = 74.62m,
            FifityfiveToFiftynine = 74.1m,
            SixtyToSixtyfour = 74.1m,
            SixtyfiveToSixtynine = 74.1m,
            SeventyToSeventyfour = 74.1m,
            SeventyfiveToSeventynine = 74.1m,
            EightyPlus = 74.1m 
        };
        
        // act
        var result = _calculator.RunFgmCalculation(survey, targetYear, YongerExtrapolationMethod.FiveYear,
            OlderExtrapolationMethod.Flatten);
        
        Assert.Multiple(() =>
        {
            Assert.That(result.YearOfSurvey, Is.EqualTo(expectedCalculatedSurvey.YearOfSurvey));
            Assert.That(result.Name, Is.EqualTo(expectedCalculatedSurvey.Name));
            Assert.That(result.CountryCode, Is.EqualTo(expectedCalculatedSurvey.CountryCode));
            Assert.That(result.CountryName, Is.EqualTo(expectedCalculatedSurvey.CountryName));
            Assert.That(result.ZeroToFour, Is.EqualTo(expectedCalculatedSurvey.ZeroToFour));
            Assert.That(result.FiveToNine, Is.EqualTo(expectedCalculatedSurvey.FiveToNine));
            Assert.That(result.TenToForteen, Is.EqualTo(expectedCalculatedSurvey.TenToForteen));
            Assert.That(result.FifteenToNineteen, Is.EqualTo(expectedCalculatedSurvey.FifteenToNineteen));
            Assert.That(result.TwentyToTwentyfour, Is.EqualTo(expectedCalculatedSurvey.TwentyToTwentyfour));
            Assert.That(result.TwentyfiveToTwentynine, Is.EqualTo(expectedCalculatedSurvey.TwentyfiveToTwentynine));
            Assert.That(result.ThirtyToThirtyfour, Is.EqualTo(expectedCalculatedSurvey.ThirtyToThirtyfour));
            Assert.That(result.ThirtyfiveToThitrynine, Is.EqualTo(expectedCalculatedSurvey.ThirtyfiveToThitrynine));
            Assert.That(result.FourtyToFourtyfour, Is.EqualTo(expectedCalculatedSurvey.FourtyToFourtyfour));
            Assert.That(result.FourtyfiveToFourtynine, Is.EqualTo(expectedCalculatedSurvey.FourtyfiveToFourtynine));
            Assert.That(result.FifteyToFifteyfour, Is.EqualTo(expectedCalculatedSurvey.FifteyToFifteyfour));
            Assert.That(result.FifityfiveToFiftynine, Is.EqualTo(expectedCalculatedSurvey.FifityfiveToFiftynine));
            Assert.That(result.SixtyToSixtyfour, Is.EqualTo(expectedCalculatedSurvey.SixtyToSixtyfour));
            Assert.That(result.SixtyfiveToSixtynine, Is.EqualTo(expectedCalculatedSurvey.SixtyfiveToSixtynine));
            Assert.That(result.SeventyToSeventyfour, Is.EqualTo(expectedCalculatedSurvey.SeventyToSeventyfour));
            Assert.That(result.SeventyfiveToSeventynine, Is.EqualTo(expectedCalculatedSurvey.SeventyfiveToSeventynine));
            Assert.That(result.EightyPlus, Is.EqualTo(expectedCalculatedSurvey.EightyPlus));
        });
    }
}