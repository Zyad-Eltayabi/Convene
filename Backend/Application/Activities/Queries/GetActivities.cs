using Application.Core;
using Domain.Entities;
using Infrastructure.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Activities.Queries;

public class GetActivities
{
    public record Query : IRequest<Result<List<Activity>>>
    {
    }
    public class Handler(ApplicationDbContext context) : IRequestHandler<Query, Result<List<Activity>>>
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<List<Activity>>> Handle(Query request, CancellationToken cancellationToken)
        {
            List<Activity>? activities = await _context.Activities.ToListAsync(cancellationToken);
            return activities is null || activities.Count == 0
                ? Result<List<Activity>>.Failure("No activities found", 404)
                : Result<List<Activity>>.Success(activities);
        }
    }
}
