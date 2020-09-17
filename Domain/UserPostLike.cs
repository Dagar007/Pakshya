using System;

namespace Domain
{
    public class UserPostLike
    {
        public string AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public Guid  PostId { get; set; }
        public virtual Post Post { get; set; }
        public DateTime DateLiked { get; set; }
        public bool IsAuthor { get; set; }
        public bool IsLiked { get; set; }
    }
}