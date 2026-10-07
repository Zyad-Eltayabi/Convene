using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Repositories;

public class UserAccessor(IHttpContextAccessor httpContext,ApplicationDbContext dbContext) : IUserAccessor
{
    public async Task<User> GetUserAsync()
    {
        return await dbContext.Users.FindAsync(GetUserId())
            ?? throw new UnauthorizedAccessException("User not found.");
    }

    public string GetUserId()
    {
        return httpContext.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? throw new Exception("User ID not found in claims.");
    }
}