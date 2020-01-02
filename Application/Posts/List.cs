using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts
{
    public class List
    {
        public class PostsEnvelope
        {
            public List<PostDto> Posts { get; set; }
            public int PostCount { get; set; }
        }
        public class Query : IRequest<PostsEnvelope>
        {
            public Query(int? limit, int? offset)
            {
                Limit = limit;
                Offset = offset;

            }
            public int? Limit { get; set; }
            public int? Offset { get; set; }
        }
        public class Handler : IRequestHandler<Query, PostsEnvelope>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<PostsEnvelope> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = _context.Posts.AsQueryable();
                var posts = await queryable
                    .Skip(request.Offset ?? 0)
                    .Take(request.Limit ?? 3).ToListAsync();
                Thread.Sleep(5000);
                return new PostsEnvelope
                {
                    Posts = _mapper.Map<List<Post>, List<PostDto>>(posts),
                    PostCount = queryable.Count()
                };
                //var postToReturn = _mapper.Map<Post, PostDto>(post);
            }
        }
    }
}