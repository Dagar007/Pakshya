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
                    throw new RestException(HttpStatusCode.NotFound, new {Comment = "Cann't find comment."});
                var user = await _context.Users.SingleOrDefaultAsync( x=> x.UserName == _userAccessor.GetUserName());

                var like = await _context.UserComments.SingleOrDefaultAsync(x => x.CommentId == comment.Id && x.AppUserId == user.Id);

                if(like != null)
                    throw new RestException(HttpStatusCode.BadRequest, new {Attendence = "Already Liked."});

                var newLike  = new UserComment {
                    Comment = comment,
                    AppUser = user,
                    IsAuthor = false
                };

                _context.UserComments.Add(newLike);

                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("problem liking comment.");
            }
        }
    }
}