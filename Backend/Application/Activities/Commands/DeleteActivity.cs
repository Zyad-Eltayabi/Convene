using Application.Core;
using Infrastructure.Persistence.Data;
using MediatR;

namespace Application.Activities.Commands;

public class DeleteActivity
{
    public record Command : IRequest<Result<Unit>>
    {
        public required string Id { get; init; }
    }

    public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities.FindAsync(request.Id, cancellationToken);
            if (activity is null)
            {
                return Result<Unit>.Failure("Activity not found", 404);
            }

            context.Activities.Remove(activity);
            var result = await context.SaveChangesAsync(cancellationToken) > 0;
            if(!result)
            {
                return Result<Unit>.Failure("Failed to delete activity");
            }
            return Result<Unit>.Success(Unit.Value);
        }
    }
}
