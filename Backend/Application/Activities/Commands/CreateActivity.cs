using Application.Activities.DTOs;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Persistence.Data;

namespace Application.Activities.Commands;

public class CreateActivity
{
    public record Command : IRequest<Result<string>>
    {
        public required CreateActivityDto Activity { get; set; } = null!;

    }

    public class Handler(ApplicationDbContext context, IMapper mapper,IUserAccessor userAccessor) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await userAccessor.GetUserAsync();
            Activity activity = mapper.Map<Activity>(request.Activity);
            context.Activities.Add(activity);
            var attendee = new ActivityAttendee
            {
                UserId = user.Id,
                ActivityId = activity.Id,
                IsHost = true
            };
            context.ActivityAttendees.Add(attendee);
            bool result = await context.SaveChangesAsync(cancellationToken) > 0;
            return !result ? Result<string>.Failure("Failed to create activity", 400) : Result<string>.Success(activity.Id);
        }
    }
}
