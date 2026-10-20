using Application.Profiles.Commands;
using Application.Profiles.DTOs;
using Application.Profiles.Queries;
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

    [HttpDelete("{photoId}/photos")]
    public async Task<IActionResult> DeletePhoto(string photoId)
    {
        return HandleResult(await Mediator.Send(
            new Application.Profiles.Commands.DeletePhoto.Command { PhotoId = photoId }));
    }
    [HttpGet("{userId}/photos")]
    public async Task<ActionResult<List<Photo>>> GetProfilePhotos(string userId)
    {
        return HandleResult(await Mediator.Send(
            new Application.Profiles.Queries.GetProfilePhotos.Query { UserId = userId }));
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<UserProfile>> GetProfile(string userId)
    {
        return HandleResult(await Mediator.Send(new GetProfile.Query { Id = userId }));
    }

    [HttpPut("{photoId}/setMain")]
    public async Task<IActionResult> SetMain(string photoId)
    {
        return HandleResult(await Mediator.Send(new SetMainPhoto.Command { Id = photoId }));
    }
}
