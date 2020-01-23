using System;

namespace Application.Profiles
{
    public class PostPostedByUserDto
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public int NoOfLikes { get; set; }
        public int NoOfComments { get; set; }
    }
}