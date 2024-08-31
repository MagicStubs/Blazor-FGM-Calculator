using System.Net.Http.Json;
using FGM.DataLayer;
using FGM.DataLayer.Interfaces;

namespace FGM.Data.ClientServices;

public class ClientSurveyService(HttpClient client) : ISurveyService
{
    public async Task<int> ImportSurveys(List<Survey> surveys)
    {
        var result = await client.PostAsJsonAsync("api/survey", surveys);
        return await result.Content.ReadFromJsonAsync<int>();
    }

    public async Task<int> DeleteSurvey(Survey surveyToDelete)
    {
        var result = await client.PostAsJsonAsync("api/survey/delete", surveyToDelete);
        return await result.Content.ReadFromJsonAsync<int>();
    }

    public async Task<int> DeleteSurveys(List<Survey> surveysToDelete)
    {
        var result = await client.PostAsJsonAsync("api/survey/deleteMultiple", surveysToDelete);
        return await result.Content.ReadFromJsonAsync<int>();
    }

    public async Task<List<Survey>> GetSurveys()
    {
        return await client.GetFromJsonAsync<List<Survey>>("api/survey");;
    }
    
    public async Task<List<Survey>> GetSurveysAuthorized()
    {
        return await client.GetFromJsonAsync<List<Survey>>("api/survey/getAuthorized");;
    }


    public async Task<int> UpdateSurvey(Survey survey)
    {
        var result = await client.PostAsJsonAsync("api/survey/update", survey);
        return await result.Content.ReadFromJsonAsync<int>();
    }
    
    public async Task<int> AddSurvey(Survey survey)
    {
        var result = await client.PostAsJsonAsync("api/survey/add", survey);
        return await result.Content.ReadFromJsonAsync<int>();
    }
}