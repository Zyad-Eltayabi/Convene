using Application.Accounts.Commands;
using Application.Accounts.DTOs;
using Application.Accounts.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers;

public class AccountsController : BaseApiController
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<string>> Register(RegisterDto registerDto)
    {
        return HandleResult(await Mediator.Send(new Register.Command { RegisterDto = registerDto }));
    }

    [AllowAnonymous]
    [HttpGet("userinfo")]
    public async Task<ActionResult<UserInfoDto>> GetUserInfo()
    {
        if (User.Identity?.IsAuthenticated != true)
        {
            return Unauthorized();
        }

        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        var result = await Mediator.Send(new GetUserInfo.Query(userId));

        if (result.Value is null)
        {
            return Unauthorized();
        }

        return Ok(result.Value);
    }
}
