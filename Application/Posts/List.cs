using System;
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
            public Query(int? limit, int? offset, string categories, string username)
            {
                Limit = limit;
                Offset = offset;
                Categories = categories;
                Username = username;

            }
            public int? Limit { get; set; }
            public int? Offset { get; set; }
            public string Categories { get; set; }  = null;
            public string Username { get; set; } =null;
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
                var queryable = _context.Posts
                    .OrderByDescending(d => d.Date)
                .AsQueryable();

                if(!string.IsNullOrEmpty(request.Categories)) 
                {
                    queryable = queryable.Where(c => c.Category.Id == request.Categories);
                }
                if(!string.IsNullOrEmpty(request.Username))
                {
                    queryable = queryable.Where(u => u.UserPosts.Any(x => x.AppUser.UserName == request.Username));
                }

                var posts = await queryable
                    .Skip(request.Offset ?? 0)
                    .Take(request.Limit ?? 3).ToListAsync();
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