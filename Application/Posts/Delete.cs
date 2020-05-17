using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var post = await _context.Posts.FindAsync(request.Id);
                if (post == null)
                    throw new RestException(HttpStatusCode.NotFound, new { post = "Not found" });
                //_context.Remove(post);
                
                var comments = await _context.Comments.Where(c => c.PostId == post.Id).ToListAsync();
                foreach(var comment in comments)
                {
                    comment.IsActive = false;
                }
                post.IsActive = false;
                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("problem saving new post.");
            }
        }
    }
}