using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.Comments;
using Domain;

namespace Application.Posts
{
    public class PostDto
    {
        
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string  Description  { get; set; }
        public Category Category { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public int For { get; set; }
        public int Against { get; set; }

        [JsonPropertyName("engagers")]
        public ICollection<LikeDto> UserPosts { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}