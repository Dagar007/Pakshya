using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Comments
{
    public class UnlikeComment
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
                if (comment == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Comment = "Can't find comment." });
                var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == _userAccessor.GetEmail(), cancellationToken: cancellationToken);

                var userCommentOrLike = await _context.UserCommentLikes.SingleOrDefaultAsync(x => x.CommentId == comment.Id && x.AppUserId == user.Id, cancellationToken: cancellationToken);


                if (userCommentOrLike == null || !userCommentOrLike.IsLiked)
                    throw new RestException(HttpStatusCode.BadRequest, new { Post = "You can only unlike after liking a post." });
                else if (!userCommentOrLike.IsAuthor)
                    _context.UserCommentLikes.Remove(userCommentOrLike);
                else
                    userCommentOrLike.IsLiked = false;


                var success = await _context.SaveChangesAsync(cancellationToken) > 0;
                if (success) return Unit.Value;

                throw new Exception("problem un-liking comment.");
            }
        }
    }
}