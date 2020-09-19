using System;
namespace Application.Comments
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public  bool Support { get; set; }
        public bool Against { get; set; }
        public int TotalLikes { get; set; }
        public bool HasLoggedInUserLiked { get; set; }
        public DateTime Date { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorDisplayName { get; set; }
        public string AuthorImage { get; set; }
       
    }
}