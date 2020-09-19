using System;
using System.Collections.Generic;
using Domain;

namespace Application.Posts
{
    public class PostConcise
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string  Description  { get; set; }
        public PostCategoryDto Category { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<Photo> Photos { get; set; }
        public string HostUsername {get; set;}
        public string HostDisplayName {get; set;}
        public string HostImage {get; set;}
        public bool IsAuthor { get; set; }
        public bool IsPostLiked { get; set; }
        public int NoOfLikes { get; set; }
        public int NoOfComments  { get; set; }
    }
}