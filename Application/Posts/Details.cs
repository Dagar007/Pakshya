using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Persistence;

namespace Application.Posts
{
    public class Details
    {
        public class Query : IRequest<PostConcise>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, PostConcise>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<Handler> _logger;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext context, IMapper mapper, ILogger<Handler> logger, IUserAccessor userAccessor)
            {
                _mapper = mapper;
                _logger = logger;
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<PostConcise> Handle(Query request, CancellationToken cancellationToken)
            {
                _logger.LogDebug("Received a request for details of post {Request}", request.GetLoggingData(_userAccessor.GetEmail()));
                var post = await _context.Posts
                .FindAsync(request.Id);
                if (post == null || !post.IsActive)
                    throw new RestException(HttpStatusCode.NotFound, new { post = "Not found" });
                var postToReturn = _mapper.Map<Post,PostConcise>(post);
                post.Views ++;
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogDebug("Returned Post {post}", postToReturn);

                return postToReturn;
            }
        }
    }
}