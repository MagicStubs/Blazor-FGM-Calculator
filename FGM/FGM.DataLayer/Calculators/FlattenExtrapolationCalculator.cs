using FGM.DataLayer.Interfaces;

namespace FGM.DataLayer.Calculators;

public class FlattenExtrapolationCalculator(IExtrapolationService service) : IOlderExtrapolationCalculator
{
    public decimal[] CalculateOlderAgeGroups(CalculatedSurvey survey)
    {
        var startingIndex = service.FindLastFilledIndex(survey.TempAgeData);

        for (var i = startingIndex + 1; i < survey.TempAgeData.Length; i++)
        {
            survey.TempAgeData[i] = survey.OrigonalSurvey!.FourtyfiveToFourtynine;
        }

        return survey.TempAgeData;
    }
}