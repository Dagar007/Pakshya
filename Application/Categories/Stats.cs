using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Categories
{
    public class Stats
    {
        public class Query : IRequest<List<CategoryStatsDto>>
        {

        }

        public class Handler : IRequestHandler<Query, List<CategoryStatsDto>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<CategoryStatsDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var post = await _context.Posts.GroupBy(c => c.Category.Value)
                    .Select(s => new CategoryStatsDto
                    {
                        Name = !string.IsNullOrEmpty(s.Key) ? s.Key : "No Category",
                        NoOfPosts = s.Count()
                    }).OrderByDescending(o => o.NoOfPosts)
                    .Take(5).ToListAsync(cancellationToken: cancellationToken);
                return post;
            }
        }
    }
}