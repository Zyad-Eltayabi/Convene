using Domain.Entities;
using Infrastructure.Persistence.Data;
using MediatR;

namespace Application.Activities.Commands;

public class EditActivity
{
    public class Command : IRequest<Unit>
    {
        public required Activity Activity { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(request.Activity.Id , cancellationToken);
            if (activity is null)
            {
                throw new Exception("Activity not found");
            }

            activity.Title = request.Activity.Title;
            activity.Description = request.Activity.Description;
            activity.Category = request.Activity.Category;
            activity.Date = request.Activity.Date;
            activity.City = request.Activity.City;
            activity.Venue = request.Activity.Venue;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
