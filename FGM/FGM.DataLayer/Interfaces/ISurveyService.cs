using Microsoft.AspNetCore.Components.Forms;

namespace FGM.DataLayer.Interfaces;

public interface ISurveyService
{
    Task<int> ImportSurveys(List<Survey> surveys);

    Task<int> DeleteSurvey(Survey surveyToDelete);

    Task<int> DeleteSurveys(List<Survey> surveysToDelete);

    Task<List<Survey>> GetSurveys();

    Task<List<Survey>> GetSurveysAuthorized();

    Task<int> UpdateSurvey(Survey survey);

    Task<int> AddSurvey(Survey survey);
}