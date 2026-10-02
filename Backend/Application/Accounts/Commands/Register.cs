using Application.Accounts.DTOs;
using Application.Core;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Accounts.Commands;

public class Register
{
    public record Command : IRequest<Result<string>>
    {
        public required RegisterDto RegisterDto { get; set; } = null!;
    }

    public class Handler(SignInManager<User> signInManager) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            RegisterDto registerDto = request.RegisterDto;
            User user = new()
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email
            };

            IdentityResult result = await signInManager.UserManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                string errors = string.Join("; ", result.Errors.Select(error => error.Description));
                return Result<string>.Failure(errors);
            }

            return Result<string>.Success(user.Id);
        }
    }
}
