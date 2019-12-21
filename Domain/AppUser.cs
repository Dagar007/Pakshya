using System;
using Microsoft.AspNetCore.Identity;

namespace Domain 
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
    }
}