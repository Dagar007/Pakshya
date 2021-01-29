using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace Domain 
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Bio { get; set; }
        public string Education { get; set; }
        public string Work { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public virtual ICollection<UserPostLike> UserPostLikes { get; set; }
        public virtual ICollection<UserCommentLike> UserCommentLikes { get; set; }
        public virtual ICollection<Photo> Photos { get; set; } = new Collection<Photo>();
        public virtual ICollection<UserFollowing> Followings { get; set; }
        public virtual ICollection<UserFollowing> Followers { get; set; }
        public virtual ICollection<UserInterest> UserInterests {get; set;}
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
        public bool IsActive { get; set; }

    }
}