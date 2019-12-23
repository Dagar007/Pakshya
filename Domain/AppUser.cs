using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain 
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string Bio { get; set; }
        public virtual ICollection<UserPost> UserPosts { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
    }
}