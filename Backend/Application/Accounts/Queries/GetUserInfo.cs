using Application.Accounts.DTOs;
using Application.Core;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Accounts.Queries;

public class GetUserInfo
{
    public record Query(string UserId) : IRequest<Result<UserInfoDto?>>;

    public class Handler(UserManager<User> userManager) : IRequestHandler<Query, Result<UserInfoDto?>>
    {
        public async Task<Result<UserInfoDto?>> Handle(Query request, CancellationToken cancellationToken)
        {
            User? user = await userManager.FindByIdAsync(request.UserId);

            if (user is null)
            {
                return Result<UserInfoDto?>.Success(null);
            }

            UserInfoDto userInfo = new()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Id = user.Id,
                ImageUrl = user.ImageUrl
            };

            return Result<UserInfoDto?>.Success(userInfo);
        }
    }
}
