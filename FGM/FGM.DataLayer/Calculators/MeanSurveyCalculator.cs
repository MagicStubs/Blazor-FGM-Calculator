using FGM.DataLayer.Interfaces;

namespace FGM.DataLayer.Calculators;

public class MeanSurveyCalculator : IMeanSurveyCalculator
{
    public CalculatedSurvey CalculateMeanSurvey(List<CalculatedSurvey> surveys)
    {
        if (surveys.Count == 0)
            throw new InvalidOperationException("No Surveys Supplied");
        
        var result = new CalculatedSurvey()
        {
            Name = "Mean Survey",
            CountryCode = surveys[0].CountryCode,
            CountryName = surveys[0].CountryName,
            YearOfSurvey = surveys[0].YearOfSurvey
        };

        foreach (var survey in surveys)
        {
            survey.PopulateTempData();
            result.TempAgeData = AddTempDataTogether(result.TempAgeData, survey.TempAgeData);
        }

        result.TempAgeData = DiviedTempData(result.TempAgeData, surveys.Count);
        result.SaveTempAgeData();

        return result;
    }

    private decimal[] AddTempDataTogether(decimal[] first, decimal[] second)
    {
        for (int i = 0; i < first.Length; i++)
        {
            first[i] += second[i];
        }

        return first;
    }

    private decimal[] DiviedTempData(decimal[] tempAgeData, int divisor)
    {
        for (int i = 0; i < tempAgeData.Length; i++)
        {
            tempAgeData[i] /= divisor;
        }

        return tempAgeData;
    }
}