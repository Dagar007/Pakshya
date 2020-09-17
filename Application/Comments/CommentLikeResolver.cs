using System.Linq;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Comments
{
    public class CommentLikeResolver : IValueResolver<Comment, CommentDto, bool>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        public CommentLikeResolver(DataContext context, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _context = context;

        }

        public bool Resolve(Comment source, CommentDto destination, bool destMember, ResolutionContext context)
        {
           var currentUser = _context.Users
                .SingleOrDefaultAsync(u => u.Email == _userAccessor.GetEmail()).Result;
            if(_context.UserCommentLikes.Any(x=> x.AppUserId == currentUser.Id && x.CommentId == source.Id))
            {
                return true;
            }
            return false;

        }
    }
}