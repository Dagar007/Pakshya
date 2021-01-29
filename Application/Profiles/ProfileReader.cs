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

        public async Task<Profile> ReadProfile(string id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (user == null)
                throw new RestException(HttpStatusCode.NotFound, new { User = "User not found." });
            var userPosts = await _context.UserPostLikes.Where(u => u.AppUser.Email == user.Email && u.IsAuthor).Take(5).ToListAsync();
            var userComments = await _context.UserCommentLikes.Where(u => u.AppUser.Email == user.Email && u.IsAuthor).Take(5).ToListAsync();
            var currentUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == _userAccessor.GetEmail());

            
            var profile = new Profile
            {
                Id = user.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Image = user.Photos?.FirstOrDefault(x => x.IsMain)?.Url,
                Photos = user.Photos,
                Bio = user.Bio,
                Education = user.Education,
                Work = user.Work,
                FollowersCount = user.Followers.Count(),
                FollowingCount = user.Followings.Count(),
                Interests = _mapper.Map<ICollection<UserInterest>,ICollection<InterestDto>>(user.UserInterests),
                Views = userPosts.Sum(u => u.Post.Views),
                Posts = _mapper.Map<List<UserPostLike>, List<PostPostedByUserDto>>(userPosts),
                Comments = _mapper.Map<List<UserCommentLike>, List<CommentPostedByUserDto>>(userComments),
                Followers = await GetFollowingList(user.Id, "followers"),
                Followings = await GetFollowingList(user.Id, "following")
            };
            if (currentUser.Followings.Any(x => x.TargetId == user.Id))
                profile.IsFollowed = true;
            return profile;

        }
        private async Task<List<FollowingUsersDto>> GetFollowingList(string id, string predicate)
        {
            var queryable = _context.Followings.AsQueryable();

            List<UserFollowing> userFollowing;
            var profiles = new List<FollowingUsersDto>();

            switch (predicate)
            {
                case "followers":
                    {
                        userFollowing = await queryable
                            .Where(x => x.Target.Id == id).ToListAsync();
                        // List of users current user is following. User who ate in target of username

                        foreach (var follower in userFollowing)
                        {
                            profiles.Add
                            (
                                new FollowingUsersDto {
                                    Id = follower.Observer.Id,
                                    DisplayName = follower .Observer.DisplayName,
                                    Url = follower.Observer.Photos?.FirstOrDefault(x => x.IsMain)?.Url
                                }
                            );
                        }
                        break;
                    }
                case "following":
                    {
                        userFollowing = await queryable
                            .Where(x => x.Observer.Id == id).ToListAsync();

                        foreach (var follower in userFollowing)
                        {
                            profiles.Add
                            (
                            new FollowingUsersDto {
                                    Id = follower.Target.Id,
                                    DisplayName = follower.Target.DisplayName,
                                    Url = follower.Target.Photos?.FirstOrDefault(x => x.IsMain)?.Url
                                }
                            );
                        }
                        break;
                    }
            }
            return profiles;
        }
    }
}