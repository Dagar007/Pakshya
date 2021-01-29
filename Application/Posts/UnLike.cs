using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts
{
    public class UnLike
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var post = await _context.Posts.FindAsync(request.Id);
                if (post == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Post = "Cann't find post." });
                var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == _userAccessor.GetEmail(), cancellationToken: cancellationToken);

                var userPostOrLike = await _context.UserPostLikes.SingleOrDefaultAsync(x => x.PostId == post.Id && x.AppUserId == user.Id, cancellationToken: cancellationToken);


                if (userPostOrLike == null || !userPostOrLike.IsLiked )
                     throw new RestException(HttpStatusCode.BadRequest, new { Post = "You can only unlike after liking a post."} );
                else if(!userPostOrLike.IsAuthor)
                     _context.UserPostLikes.Remove(userPostOrLike);
                else
                    userPostOrLike.IsLiked = false;

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem un-liking new post.");
            }
        }
    }
}