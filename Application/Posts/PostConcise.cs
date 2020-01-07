using System;
using Domain;

namespace Application.Posts
{
    public class PostConcise
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string  Description  { get; set; }
        public Category Category { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public string HostUsername {get; set;}
        public string HostDisplayName {get; set;}
        public string HostImage {get; set;}
        public bool IsAuthor { get; set; }
        public bool IsPostLiked { get; set; }
        public int NoOfLikes { get; set; }
        public int NoOfComments  { get; set; }

        // [JsonPropertyName("engagers")]
        // public ICollection<LikeDto> UserPosts { get; set; }
        // public ICollection<CommentDto> Comments { get; set; }
    }
}