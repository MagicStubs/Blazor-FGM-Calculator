namespace FGM.DataLayer.Interfaces;

public interface IOlderExtrapolationCalculator
{
    decimal[] CalculateOlderAgeGroups(CalculatedSurvey survey);
}