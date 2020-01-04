using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts
{
    public class MostDiscussedPost
    {
        public class Query : IRequest<List<MostDisscussedPostDto>>
        {

        }

        public class Handler : IRequestHandler<Query, List<MostDisscussedPostDto>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<MostDisscussedPostDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var post = await _context.Posts.OrderByDescending(x => x.Comments.Count)
                                .Take(5)
                               .Select(s => new MostDisscussedPostDto
                               {
                                   PostId = s.Id,
                                   Heading = s.Heading.Length < 25? s.Heading: $"{s.Heading.Substring(0, 25)}...",
                                   NoOfComments = s.Comments.Count
                               }).ToListAsync();
                return post;
            }


        }
    }
}