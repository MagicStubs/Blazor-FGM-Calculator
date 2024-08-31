using FGM.Data;
using FGM.DataLayer.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace FGM.DataLayer.Services;

public class SurveyService(FgmDbContext context) : ISurveyService
{
    public async Task<int> ImportSurveys(List<Survey> surveys)
    {
        context.Surveys.AddRange(surveys);
        return await context.SaveChangesAsync();
    }

    public Task<int> DeleteSurvey(Survey surveyToDelete)
    {
        context.Surveys.Remove(surveyToDelete);
        return context.SaveChangesAsync();
    }

    public Task<int> DeleteSurveys(List<Survey> surveysToDelete)
    {
        context.Surveys.RemoveRange(surveysToDelete);
        return context.SaveChangesAsync();
    }

    public Task<List<Survey>> GetSurveys()
    {
        return context.Surveys.ToListAsync();
    }

    public Task<List<Survey>> GetSurveysAuthorized()
    {
        return context.Surveys.ToListAsync();
    }

    public Task<int> UpdateSurvey(Survey survey)
    {
        context.Surveys.Update(survey);
        return context.SaveChangesAsync();
    }

    public Task<int> AddSurvey(Survey survey)
    {
        context.Surveys.Add(survey);
        return context.SaveChangesAsync();
    }
}