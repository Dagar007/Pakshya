using System;
using System.Collections.Generic;

namespace Domain
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string  Description  { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public int For { get; set; }
        public int Against { get; set; }

        public virtual ICollection<UserPost> UserPosts { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}