using Domain.Entities;
using Infrastructure.Persistence.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Activities.Queries;

public class GetActivities
{
    public record Query : IRequest<List<Activity>>
    {
    }
    public class Handler(ApplicationDbContext context) : IRequestHandler<Query, List<Activity>>
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _context.Activities.ToListAsync(cancellationToken);
        }
    }
}
