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
} 
