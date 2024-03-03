using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Persistence;

namespace Application.Posts
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IBlobService _blobService;
            public Handler(DataContext context, IBlobService blobService)
            {
                _context = context;
                _blobService = blobService;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var post = await _context.Posts.FindAsync(request.Id);
                if (post == null)
                    throw new RestException(HttpStatusCode.NotFound, new { post = "Not found" });
                if (post.Photos.Count > 0)
                {
                    await _blobService.DeleteBlobAsync(post.Photos.First().Url.Split("/").Last());
                }
                _context.Remove(post);

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;
                if (success) return Unit.Value;

                throw new Exception("problem saving new post.");
            }
        }
    }
}