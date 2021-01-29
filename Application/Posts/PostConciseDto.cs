using System;
using Domain;

namespace Application.Posts
{
    public class PostConciseDto
    {
         public Guid Id { get; set; }
        public string Heading { get; set; }
        public string  Description  { get; set; }
        public Category Category { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public bool IsPostLikedByUser { get; set; }
        public int NoOfComments { get; set; }
        public int NoOfLikes { get; set; }
    }
}