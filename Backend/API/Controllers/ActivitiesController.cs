
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

class activity
{
    public string id { get; set; }
    public string title { get; set; }
}
public class ActivitiesController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetActivities()
    {
        List<activity> activities = new();
        activities.Add(new activity { id = "1", title = "Activity 1" });
        activities.Add(new activity { id = "2", title = "Activity 2" });
        activities.Add(new activity { id = "3", title = "Activity 3" });
        return Ok(activities);
    }
} 
