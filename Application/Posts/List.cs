using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Helpers;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Application.Posts
{
    public class List
    {
        public class Query : IRequest<List<PostConcise>>
        {
            public Query(Params postParams)
            {
                this.PostParams = postParams;
            }
            public Params PostParams { get; set; }
        }
        public class Handler : IRequestHandler<Query, List<PostConcise>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;
            public Handler(DataContext context, IMapper mapper, IHttpContextAccessor httpContext)
            {
                _httpContext = httpContext;
                _mapper = mapper;
                _context = context;
            }

            public async Task<List<PostConcise>> Handle(Query request, CancellationToken cancellationToken)
            {
                var queryable = _context.Posts.Where(x => x.IsActive)
                    .OrderByDescending(d => d.Date)
                .AsQueryable();

                if(!string.IsNullOrEmpty(request.PostParams.Category)) 
                {
                    queryable = queryable.Where(c => c.Category.Id == request.PostParams.Category);
                }
               
                // if(!string.IsNullOrEmpty(request.Username))
                // {
                //     queryable = queryable.Where(u => u.UserPosts.Any(x => x.AppUser.UserName == request.Username));
                // }

                var posts = await PagedList<Post>.CreateAsync(queryable, request.PostParams.PageNumber, request.PostParams.PageSize);
                _httpContext.HttpContext.Response.AddPagination(posts.CurrentPage, posts.PageSize,posts.TotalCount, posts.TotalPages);
                return _mapper.Map<List<Post>, List<PostConcise>>(posts);

                
            }
        }
    }
}