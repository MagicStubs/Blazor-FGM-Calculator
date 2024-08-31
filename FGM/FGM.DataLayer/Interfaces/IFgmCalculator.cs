using FGM.Data.Enums;

namespace FGM.DataLayer.Interfaces;

public interface IFgmCalculator
{
    decimal CalculateTemp10To14(Survey survey);

    decimal CalculateTemp50To54(Survey survey);

    int CalculateDifference(int targetYear, int surveyYear);

    int CalculateQuotientAndRemainder(int difference, out int remainder);

    public decimal[] CalculateShiftedAgeData(Survey survey, int targetYear);

    public decimal[] CalculateYongerAgeGroups(decimal[] ageData, YongerExtrapolationMethod method);

    public decimal[] CalculateOlderAgeGroups(CalculatedSurvey survey, OlderExtrapolationMethod method);

    public CalculatedSurvey RunFgmCalculation(Survey survey, int targetYear, YongerExtrapolationMethod yongerExtrapolationMethod,
        OlderExtrapolationMethod olderExtrapolationMethod);
}