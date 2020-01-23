using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles
{
    public class ProfileReader : IProfileReader
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;
        public ProfileReader(DataContext context, IUserAccessor userAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _userAccessor = userAccessor;
            _context = context;
        }

        public async Task<Profile> ReadProfile(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
            if (user == null)
                throw new RestException(HttpStatusCode.NotFound, new { User = "User not found." });
            var userPosts = await _context.UserPosts.Where(u => u.AppUser.UserName == user.UserName && u.IsAuthor == true).ToListAsync();
            var userComments = await _context.UserComments.Where(u => u.AppUser.UserName == user.UserName && u.IsAuthor == true).ToListAsync();
            var currentUser = await _context.Users.SingleOrDefaultAsync(u => u.UserName == _userAccessor.GetUserName());
            var categories = await _context.Categories.ToListAsync();
            List<InterestDTO> allInterests = new List<InterestDTO>();
            foreach (var c in categories)
            {
                allInterests.Add(new InterestDTO { Id = c.Id, Value = c.Value, DoesUser = false });
            }
            if (!string.IsNullOrEmpty(user.Interests))
            {
                foreach (var c in allInterests)
                {
                    foreach (var i in user.Interests.Split(','))
                    {
                        if (c.Id == i)
                        {
                            allInterests.Find(io => io.Id == i).DoesUser = true;
                        }
                    }
                }
            }
            var profile = new Profile
            {
                DisplayName = user.DisplayName,
                Username = user.UserName,
                Image = user.Photos?.FirstOrDefault(x => x.IsMain)?.Url,
                Photos = user.Photos,
                Bio = user.Bio,
                Address = user.Address,
                Education = user.Education,
                Work = user.Work,
                FollowersCount = user.Followers.Count(),
                FollowingCount = user.Followings.Count(),
                Interests = allInterests,
                Posts = _mapper.Map<List<UserPost>,List<PostPostedByUserDto>>(userPosts),
                Comments = _mapper.Map<List<UserComment>,List<CommentPostedByUserDto>>(userComments),
                Followers = _mapper.Map<List<UserFollowing>,List<FollowingUsersDTO>>(user.Followers.ToList()),
                Followings=  _mapper.Map<List<UserFollowing>,List<FollowingUsersDTO>>(user.Followings.ToList()),
            };
            if (currentUser.Followings.Any(x => x.TargetId == user.Id))
                profile.IsFollowed = true;
            return profile;

        }
    }
}