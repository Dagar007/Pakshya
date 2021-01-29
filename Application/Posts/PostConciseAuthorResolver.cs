using System.Linq;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts
{
    public class PostConciseAuthorResolver : IValueResolver<Post, PostConcise, bool>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public PostConciseAuthorResolver(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        public bool Resolve(Post source, PostConcise destination, bool destMember, ResolutionContext context)
        {
            var currentUser = _context.Users
            .SingleOrDefaultAsync(u => u.Email == _userAccessor.GetEmail()).Result;
            return currentUser != null && source.UserPostLikes.Any(p => p.AppUser.Email == currentUser.Email && p.IsAuthor);
        }
    }
}