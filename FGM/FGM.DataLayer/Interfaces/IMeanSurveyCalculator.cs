namespace FGM.DataLayer.Interfaces;

public interface IMeanSurveyCalculator
{
    CalculatedSurvey CalculateMeanSurvey(List<CalculatedSurvey> surveys);
}