using System.Collections.Generic;
using System.Text.Json.Serialization;
using Domain;

namespace Application.Profiles
{
    public class Profile
    {
        // General Section
        public string Id { get; set; }
        public string DisplayName { get; set; } 
        public string Email { get; set; }
        public string Image { get; set; }

        // Personal Section
        public string Bio { get; set; }
        public string Education { get; set; }
        public string Work { get; set; }
        [JsonPropertyName("following")]
        public bool IsFollowed { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public ICollection<Photo> Photos { get; set; }

        // Stats Section
        public int Views { get; set; }
        public ICollection<InterestDto> Interests { get; set; }
        public ICollection<PostPostedByUserDto> Posts { get; set; }
        public ICollection<CommentPostedByUserDto> Comments { get; set; }

        public ICollection<FollowingUsersDto> Followings { get; set; }
        public ICollection<FollowingUsersDto> Followers { get; set; }
        
    }
}