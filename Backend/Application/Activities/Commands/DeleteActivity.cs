using Infrastructure.Persistence.Data;
using MediatR;

namespace Application.Activities.Commands;

public class DeleteActivity
{
    public record Command : IRequest<Unit>
    {
        public required string Id { get; init; }
    }

    public class Handler(ApplicationDbContext context) : IRequestHandler<Command, Unit>
    {
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities.FindAsync(request.Id, cancellationToken);
            if (activity is null)
            {
                throw new Exception("Activity not found");
            }

            context.Activities.Remove(activity);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
