using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Helpers;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Posts
{
    public class List
    {
        public class Query : IRequest<List<PostConcise>>
        {
            public Query(Params postParams)
            {
                PostParams = postParams;
            }
            public Params PostParams { get; set; }
        }
        public class Handler : IRequestHandler<Query, List<PostConcise>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpContext;
            private readonly ILogger<Handler> _logger;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper, IHttpContextAccessor httpContext, ILogger<Handler> logger, IUserAccessor userAccessor)
            {
                _httpContext = httpContext;
                _logger = logger;
                _userAccessor = userAccessor;
                _mapper = mapper;
                _context = context;
            }

            public async Task<List<PostConcise>> Handle(Query request, CancellationToken cancellationToken)
            {
                _logger.LogDebug("Received a request for list of post with {Request}",request.PostParams.GetLoggingData(_userAccessor.GetEmail()));
                //throw new Exception("test exception");
                var queryable = _context.Posts.Where(x => x.IsActive)
                    .OrderByDescending(d => d.Date)
                .AsQueryable();

                if(request.PostParams.Category != Guid.Empty) 
                {
                    queryable = queryable.Where(c => c.Category.Id == request.PostParams.Category);
                }

                var posts = await PagedList<Post>.CreateAsync(queryable, request.PostParams.PageNumber, request.PostParams.PageSize);
                _httpContext.HttpContext?.Response.AddPagination(posts.CurrentPage, posts.PageSize,posts.TotalCount, posts.TotalPages);
                var postsToReturn =  _mapper.Map<List<Post>, List<PostConcise>>(posts);

                _logger.LogDebug("Found {PostCount} posts in database", postsToReturn.Count);

                return postsToReturn;
            }
        }
    }
}