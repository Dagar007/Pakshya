using System;

namespace Application.Profiles
{
    public class CommentPostedByUserDto
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public int NoLikes { get; set; }
    }
}