using System.Linq;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Posts
{
    public class FollowingResolver : IValueResolver<UserPost, LikeDto, bool>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        public FollowingResolver(DataContext context, IUserAccessor userAccessor)
        {
            _userAccessor = userAccessor;
            _context = context;
        }

        public bool Resolve(UserPost source, LikeDto destination, bool destMember, ResolutionContext context)
        {
            var currentUser = _context.Users
                .SingleOrDefaultAsync(u => u.UserName == _userAccessor.GetUserName()).Result;
            if(currentUser.Followings.Any(c => c.TargetId == source.AppUser.Id))
            {
                return true;
            }
            return false;
        }
    }
}