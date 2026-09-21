using Application.Activities.DTOs;
using Application.Core;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Data;
using MediatR;

namespace Application.Activities.Commands;

public class CreateActivity
{
    public record Command : IRequest<Result<string>>
    {
        public required CreateActivityDto Activity { get; set; } = null!;

    }

    public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Command, Result<string>>
    {
        public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
        {
            Activity activity = mapper.Map<Activity>(request.Activity);
            context.Activities.Add(activity);
            bool result = await context.SaveChangesAsync(cancellationToken) > 0;
            return !result ? Result<string>.Failure("Failed to create activity", 400) : Result<string>.Success(activity.Id);
        }
    }
}
