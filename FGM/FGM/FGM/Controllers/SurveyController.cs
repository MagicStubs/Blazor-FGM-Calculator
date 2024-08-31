using FGM.DataLayer;
using FGM.DataLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FGM.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SurveyController(ISurveyService surveyService) : ControllerBase
{
    [HttpGet]
    [ResponseCache(Duration = 60)]
    public async Task<ActionResult<List<Survey>>> GetSurveys()
    {
        return Ok(await surveyService.GetSurveys());
    }

    [HttpGet]
    [Route("getAuthorized")]
    public async Task<ActionResult<List<Survey>>> GetSurveysAuthorized()
    {
        return Ok(await surveyService.GetSurveys());
    }
    
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<int>> ImportSurveys(List<Survey> surveys)
    {
        var result = await surveyService.ImportSurveys(surveys);
        return Ok(result);
    }

    [HttpPost]
    [Route("delete")]
    [Authorize]
    public async Task<ActionResult<int>> DeleteSurvey(Survey survey)
    {
        return Ok( await surveyService.DeleteSurvey(survey));
    }

    [HttpPost]
    [Route("deleteMultiple")]    
    [Authorize]
    public async Task<ActionResult<int>> DeleteSurveys(List<Survey> surveys)
    {
        return Ok(await surveyService.DeleteSurveys(surveys));
    }

    [HttpPost]
    [Route("update")]
    [Authorize]
    public async Task<ActionResult<int>> UpdateSurvey(Survey survey)
    {
        return Ok(await surveyService.UpdateSurvey(survey));
    }
    
    [HttpPost]
    [Route("add")]
    [Authorize]
    public async Task<ActionResult<int>> AddSurvey(Survey survey)
    {
        return Ok(await surveyService.AddSurvey(survey));
    }
}