using Application.Core;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Accounts.Commands;

public class SignOut
{
    public record Command : IRequest<Result<Unit>>;

    public class Handler(SignInManager<User> signInManager) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            await signInManager.SignOutAsync();
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
