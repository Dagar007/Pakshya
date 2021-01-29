using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Comments
{
    public class LikeComment
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
                var comment = await _context.Comments.FindAsync(request.Id);
                if(comment == null)
                    throw new RestException(HttpStatusCode.NotFound, new {Comment = "Can't find comment."});
                var user = await _context.Users.SingleOrDefaultAsync( x=> x.Email == _userAccessor.GetEmail(), cancellationToken: cancellationToken);

                var userLikeOrComment = await _context.UserCommentLikes.SingleOrDefaultAsync(x => x.CommentId == comment.Id && x.AppUserId == user.Id, cancellationToken: cancellationToken);

                if (userLikeOrComment?.IsLiked == true)
                    throw new RestException(HttpStatusCode.BadRequest, new { Attendence = "Already Liked." });
                else if (userLikeOrComment?.IsLiked == false)
                    userLikeOrComment.IsLiked = true;
                else
                {
                    var newLike  = new UserCommentLike {
                    Comment = comment,
                    AppUser = user,
                    IsAuthor = false,
                    IsLiked = true
                    
                };

                await _context.UserCommentLikes.AddAsync(newLike, cancellationToken);
                }
               

                var success = await _context.SaveChangesAsync(cancellationToken) > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem liking comment.");
            }
        }
    }
}