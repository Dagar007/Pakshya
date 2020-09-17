using System.Linq;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts
{
    public class PostConsiceIsPostLikedResolver : IValueResolver<Post, PostConcise, bool>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public PostConsiceIsPostLikedResolver(DataContext context, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _context = context;

        }

        public bool Resolve(Post source, PostConcise destination, bool destMember, ResolutionContext context)
        {
            var currentUser = _context.Users
                .SingleOrDefaultAsync(u => u.Email == _userAccessor.GetEmail()).Result;
            if (currentUser == null)
                return false;
            if (source.UserPostLikes.Any(p => p.AppUser.Email == currentUser.Email && p.IsLiked == true))
            {
                return true;
            }
            return false;

        }
    }
}