using Domain.Entities;
using Infrastructure.Persistence.Data;
using MediatR;

namespace Application.Activities.Commands;

public class CreateActivity
{
    public record Command : IRequest<string>
    {
        public required Activity Activity { get; set; } = null!;
    }

    public class Handler(ApplicationDbContext context) : IRequestHandler<Command,string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            context.Activities.Add(request.Activity);
            await context.SaveChangesAsync(cancellationToken);
            return request.Activity.Id;
        }
    }
}
