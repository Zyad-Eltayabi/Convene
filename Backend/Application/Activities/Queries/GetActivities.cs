using Application.Activities.DTOs;
using Application.Core;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Activities.Queries;

public class GetActivities
{
    public record Query : IRequest<Result<List<ActivityDto>>>
    {
    }
    public class Handler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<Query, Result<List<ActivityDto>>>
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Result<List<ActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var activities = await _context.Activities.
                ProjectTo<ActivityDto>(mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            return activities is null || activities.Count == 0
                ? Result<List<ActivityDto>>.Failure("No activities found", 404)
                : Result<List<ActivityDto>>.Success(activities);
        }
    }
}
