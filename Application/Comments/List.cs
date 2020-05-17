using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Helpers;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Application.Comments
{
    public class List
    {
         public class Query : IRequest<List<CommentDto>>
        {
            public Query(Guid postId, Params postParams)
            {
                this.CommentParams = postParams;
                this.PostId = postId;
            }
            public Params CommentParams { get; set; }
            public Guid PostId { get; set; }
        }
        public class Handler : IRequestHandler<Query, List<CommentDto>>
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

            public async Task<List<CommentDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                

                var queryable = _context.Comments.Where(c => c.PostId == request.PostId && c.IsActive)
                    .OrderByDescending(d => d.Date).AsQueryable();
               
                if(queryable == null)
                     throw new RestException(HttpStatusCode.NotFound, new { post = "Not found" });
                var comments = await PagedList<Comment>.CreateAsync(queryable, request.CommentParams.PageNumber, request.CommentParams.PageSize);
                _httpContext.HttpContext.Response.AddPagination(comments.CurrentPage, comments.PageSize,comments.TotalCount, comments.TotalPages);
                return _mapper.Map<List<Comment>, List<CommentDto>>(comments);  
            }
        }
    }
}