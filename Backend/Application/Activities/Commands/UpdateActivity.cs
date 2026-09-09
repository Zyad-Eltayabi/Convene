using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Data;
using MediatR;

namespace Application.Activities.Commands;

public class UpdateActivity
{
    public record Command : IRequest<Unit>
    {
        public required Activity Activity { get; set; } = null!;
    }
    public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Command, Unit>
    {
        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            Activity? activity = await context.Activities.FindAsync(request.Activity.Id, cancellationToken);
            if (activity is null)
            {
                throw new Exception("Activity not found");
            }

            mapper.Map(request.Activity, activity);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}


