using AutoMapper;
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
        private readonly IMapper _mapper;

        public Handler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(request.Activity.Id , cancellationToken);
            if (activity is null)
            {
                throw new Exception("Activity not found");
            }

            _mapper.Map(request.Activity, activity);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
