using Application.Activities.Commands;
using Application.Activities.DTOs;
using Application.Activities.Queries;
using Domain.Entities;
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
        List<Activity> activities = await Mediator.Send(new GetActivities.Query());
        return Ok(activities);
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateNewActivity(CreateActivityDto activityDto)
    {
        string activityId = await Mediator.Send(
            new CreateActivity.Command
            {
                Activity = activityDto
            });

        return Ok(activityId);
    }

    [HttpPut()]
    public async Task<IActionResult> EditActivity(Activity activity)
    {
        await Mediator.Send(new UpdateActivity.Command { Activity = activity });
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActivity(string id)
    {
        await Mediator.Send(new DeleteActivity.Command { Id = id });
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivityDetail(string id)
    {
        return HandleResult(await Mediator.Send(new GetActivityDetails.Query { Id = id }));
    }
}
