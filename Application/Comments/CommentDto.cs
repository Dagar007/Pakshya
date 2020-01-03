using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Application.Comments
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public bool For { get; set; }
        public bool Against { get; set; }
        public int Liked { get; set; }
        public DateTime Date { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Image { get; set; }
        [JsonPropertyName("commentors")]
        public ICollection<LikeDto> UserComments { get; set; }
    }
}