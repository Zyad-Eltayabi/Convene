using Application.Activities.Commands;
using Application.Activities.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{
   public ActivitiesController()
    {
    }
    [HttpGet]
    public async Task<ActionResult<List<Activity>>> GetActivities()
    {
        List<Activity> activities = await Mediator.Send(new GetActivityList.Query());
        return Ok(activities);
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateNewActivity(Activity activity)
    {
        string activityId = await Mediator.Send(new CreateActivity.Command{ Activity = activity });
        return Ok(activityId);
    }

    [HttpPut()]
    public async Task<IActionResult> EditActivity(Activity activity)
    {
        await Mediator.Send(new Application.Activities.Commands.EditActivity.Command { Activity  = activity});
        return NoContent();
    }
} 
