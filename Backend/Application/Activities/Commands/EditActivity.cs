using Application.Core;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Data;
using MediatR;

namespace Application.Activities.Commands;

public class EditActivity
{
    public class Command : IRequest<Result<Unit>>
    {
        public required Activity Activity { get; set; } = null!;
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public Handler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            Activity? activity = await _context.Activities.FindAsync(request.Activity.Id, cancellationToken);
            if (activity is null)
            {
                return Result<Unit>.Failure("Activity not found", 404);
            }

            _mapper.Map(request.Activity, activity);
            var result = await _context.SaveChangesAsync(cancellationToken) > 0;
            return !result ? Result<Unit>.Failure("Failed to update activity", 400) : Result<Unit>.Success(Unit.Value);
        }
    }
}
