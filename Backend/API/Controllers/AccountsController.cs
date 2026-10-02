using Application.Accounts.Commands;
using Application.Accounts.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AccountsController : BaseApiController
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<string>> Register(RegisterDto registerDto)
    {
        return HandleResult(await Mediator.Send(new Register.Command { RegisterDto = registerDto }));
    }
}
