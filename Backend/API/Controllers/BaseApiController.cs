using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>() ?? throw new InvalidOperationException("Mediator not found.");

    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if(!result.IsSuccess)
        {
            if (result.StatusCode == 404)
                return NotFound(result.Error);
            else if (result.StatusCode == 400)
                return BadRequest(result.Error);
            else
                return StatusCode(result.StatusCode, result.Error);
        }
        return Ok(result.Value);
    }
}
