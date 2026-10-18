using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class ProfilesController : BaseApiController
{
    [HttpPost("add-photo")]
    public async Task<ActionResult<Photo>> AddPhoto([FromForm] IFormFile file)
    {
        return HandleResult(await Mediator.Send(
            new Application.Profiles.Commands.AddPhoto.Command { File = file }));
    }

    [HttpDelete("{photoId}")]
    public async Task<IActionResult> DeletePhoto(string photoId)
    {
        return HandleResult(await Mediator.Send(
            new Application.Profiles.Commands.DeletePhoto.Command { PhotoId = photoId }));
    }
}
