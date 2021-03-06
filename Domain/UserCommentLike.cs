using System;

namespace Domain
{
    public class UserCommentLike
    {
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public Guid CommentId { get; set; }
        public virtual Comment Comment { get; set; }
        public DateTime DateLiked { get; set; }
        public bool IsAuthor { get; set; }
        public bool IsLiked { get; set; }
        
    }
}