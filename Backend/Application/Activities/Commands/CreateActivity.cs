using Application.Activities.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Data;
using MediatR;

namespace Application.Activities.Commands;

public class CreateActivity
{
    public record Command : IRequest<string>
    {
        public required CreateActivityDto Activity { get; set; } = null!;
    }

    public class Handler(ApplicationDbContext context,IMapper mapper) : IRequestHandler<Command, string>
    {
        public async Task<string> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = mapper.Map<Activity>(request.Activity);
            context.Activities.Add(activity);
            await context.SaveChangesAsync(cancellationToken);
            return activity.Id;
        }
    }
}
