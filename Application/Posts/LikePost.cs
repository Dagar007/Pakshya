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
                if (post == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Post = "Can't find post." });
                var user = await _context.Users.SingleOrDefaultAsync(x => x.Email == _userAccessor.GetEmail(), cancellationToken: cancellationToken);

                var userLikeOrPost = await _context.UserPostLikes.SingleOrDefaultAsync(x => x.PostId == post.Id && x.AppUserId == user.Id, cancellationToken: cancellationToken);

                if (userLikeOrPost?.IsLiked == true)
                    throw new RestException(HttpStatusCode.BadRequest, new { Attendence = "Already Liked." });
                else if (userLikeOrPost?.IsLiked == false)
                    userLikeOrPost.IsLiked = true;
                else
                {
                    var newLike = new UserPostLike
                    {
                        Post = post,
                        AppUser = user,
                        IsAuthor = false,
                        IsLiked = true
                    };
                    await _context.UserPostLikes.AddAsync(newLike, cancellationToken);
                }
                var success = await _context.SaveChangesAsync(cancellationToken) > 0;
                if (success) return Unit.Value;

                throw new Exception("Problem saving new post.");
            }
        }
    }
}