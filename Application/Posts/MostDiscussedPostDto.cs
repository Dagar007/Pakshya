using System;

namespace Application.Posts
{
    public class MostDiscussedPostDto
    {
        public Guid PostId { get; set; }
        public string Heading { get; set; }
        public int NoOfComments { get; set; }
    }
}