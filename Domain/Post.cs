using System;
using System.Collections.Generic;

namespace Domain
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string  Description  { get; set; }
        public virtual Category Category {get; set;}
        public DateTime Date { get; set; }
        public int For { get; set; }
        public int Against { get; set; }
        public int Views { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<UserPost> UserPosts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public bool IsActive { get; set; }
       
    }
}