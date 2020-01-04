using System;

namespace Application.Posts
{
    public class MostDisscussedPostDto
    {
        public Guid PostId { get; set; }
        public string Heading { get; set; }
        public int NoOfComments { get; set; }
    }
}