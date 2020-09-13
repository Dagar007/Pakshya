using System.Linq;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts
{
    public class PostConsiceAuthorResolver : IValueResolver<Post, PostConcise, bool>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public PostConsiceAuthorResolver(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        public bool Resolve(Post source, PostConcise destination, bool destMember, ResolutionContext context)
        {
            var currentUser = _context.Users
            .SingleOrDefaultAsync(u => u.UserName == _userAccessor.GetUserName()).Result;
            if(currentUser == null)
                return false;
            if (source.UserPostLikes.Any(p => p.AppUser.UserName == currentUser.UserName && p.IsAuthor == true))
            {
                return true;
            }
            return false;
        }
    }
}