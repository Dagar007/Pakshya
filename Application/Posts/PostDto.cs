using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Posts
{
    public class PostDto
    {
        
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string  Description  { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public int For { get; set; }
        public int Against { get; set; }

        [JsonPropertyName("engagers")]
        public ICollection<LikeDto> UserPosts { get; set; }
    }
}