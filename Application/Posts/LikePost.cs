using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts
{
    public class LikePost
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
                if(post == null)
                    throw new RestException(HttpStatusCode.NotFound, new {Post = "Cann't find post."});
                var user = await _context.Users.SingleOrDefaultAsync( x=> x.UserName == _userAccessor.GetUserName());

                var like = await _context.UserPosts.SingleOrDefaultAsync(x => x.PostId == post.Id && x.AppUserId == user.Id);

                if(like != null)
                    throw new RestException(HttpStatusCode.BadRequest, new {Attendence = "Already Liked."});

                var newLike  = new UserPost {
                    Post = post,
                    AppUser = user,
                    IsAuthor = false
                };

                _context.UserPosts.Add(newLike);

                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;

                throw new Exception("problem saving new post.");
            }
        }
    }
}