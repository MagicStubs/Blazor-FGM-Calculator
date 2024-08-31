using System.Runtime.InteropServices.JavaScript;
using FGM.Data.Enums;
using FGM.DataLayer;
using FGM.DataLayer.Interfaces;

namespace FGM.Data.Calculators;

public class FgmCalculator(IYongerExtrapolationCalculatorFactorty yongerExtrapolationCalculatorFactorty, IOlderExtrapolationCalculatorFactorty olderExtrapolationCalculatorFactorty) : IFgmCalculator
{
    public decimal CalculateTemp10To14(Survey survey)
    {
        return survey.FifteenToNineteen / survey.TwentyToTwentyfour * survey.FifteenToNineteen;
    }

    public decimal CalculateTemp50To54(Survey survey)
    {
        return survey.FourtyfiveToFourtynine / survey.FourtyToFourtyfour * survey.FourtyfiveToFourtynine;
    }

    public int CalculateDifference(int targetYear, int surveyYear)
    {
        return targetYear - surveyYear;
    }

    public int CalculateQuotientAndRemainder(int difference, out int remainder)
    {
        return Math.DivRem(difference, 5, out remainder);
    }

    public decimal[] CalculateShiftedAgeData(Survey survey, int targetYear)
    {
        var shiftedAgeData = new decimal[17];
        var surveyYearTargetYearDifference = CalculateDifference(targetYear, survey.YearOfSurvey);

        var quotient = CalculateQuotientAndRemainder(surveyYearTargetYearDifference, out var remainder);

        var initialAgeDataWithTempValues = GetSurveyAgeDataWithTempAgeBuffers(survey);

        for (var cellIndex = 3; cellIndex < 10; cellIndex++)
        {
            var newCellIndex = cellIndex + quotient;

            if (newCellIndex < 0)
                continue;

            if (newCellIndex > shiftedAgeData.Length - 1)
                continue;

            var newCellValue = surveyYearTargetYearDifference >= 0
                ? CalculateShiftedCellValuePositiveYearDifference(initialAgeDataWithTempValues[cellIndex],
                    initialAgeDataWithTempValues[cellIndex - 1], remainder)
                : CalculateShiftedCellValueNegitiveYearDifference(initialAgeDataWithTempValues[cellIndex],
                    initialAgeDataWithTempValues[cellIndex + 1], remainder);
            shiftedAgeData[newCellIndex] = newCellValue;
        }

        return shiftedAgeData;
    }

    public decimal[] CalculateYongerAgeGroups(decimal[] ageData, YongerExtrapolationMethod method)
    {
        var extrapolationCalculator = yongerExtrapolationCalculatorFactorty.GetExtrapolationCalculator(method);

        return extrapolationCalculator.ExtrapolateYongerAgeGroups(ageData);
    }

    public decimal[] CalculateOlderAgeGroups(CalculatedSurvey calculatedSurvey, OlderExtrapolationMethod method)
    {
        var calculator = olderExtrapolationCalculatorFactorty.GetOlderExtrapolationCalculator(method);
        return calculator.CalculateOlderAgeGroups(calculatedSurvey);
    }

    public CalculatedSurvey RunFgmCalculation(Survey survey, int targetYear, YongerExtrapolationMethod yongerExtrapolationMethod,
        OlderExtrapolationMethod olderExtrapolationMethod)
    {
        var calculatedSurvey = new CalculatedSurvey()
        {
            OrigonalSurvey = survey,
            CountryCode = survey.CountryCode,
            CountryName = survey.CountryName,
            Name = survey.Name,
            YearOfSurvey = targetYear,
        };

        var tempAgeData = CalculateShiftedAgeData(survey, targetYear);
        tempAgeData = CalculateYongerAgeGroups(tempAgeData, yongerExtrapolationMethod);
        calculatedSurvey.TempAgeData = tempAgeData;
        calculatedSurvey.TempAgeData = CalculateOlderAgeGroups(calculatedSurvey, olderExtrapolationMethod);

        calculatedSurvey.SaveTempAgeData();
        
        return calculatedSurvey;
    }

    private decimal CalculateShiftedCellValueNegitiveYearDifference(decimal cellOne,
        decimal cellTwo, int remainder)
    {
        return cellOne / 5 * (5 + remainder) + cellTwo / -5 * remainder;
    }

    private decimal CalculateShiftedCellValuePositiveYearDifference(decimal cellOne, decimal cellTwo, int remainder)
    {
        return cellOne / 5 * (5 - remainder) + cellTwo / 5 * remainder;
    }

    private decimal[] GetSurveyAgeDataWithTempAgeBuffers(Survey survey)
    {
        return new decimal[17]
        {
            0m,
            0m,
            CalculateTemp10To14(survey),
            survey.FifteenToNineteen,
            survey.TwentyToTwentyfour,
            survey.TwentyfiveToTwentynine,
            survey.ThirtyToThirtyfour,
            survey.ThirtyfiveToThitrynine,
            survey.FourtyToFourtyfour,
            survey.FourtyfiveToFourtynine,
            CalculateTemp50To54(survey),
            0m,
            0m,
            0m,
            0m,
            0m,
            0m
        };
    }
}