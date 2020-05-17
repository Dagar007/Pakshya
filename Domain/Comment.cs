using System;
using System.Collections.Generic;

namespace Domain
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Body { get; set; }
        public virtual AppUser Author { get; set; }
        public bool For { get; set; }
        public bool Against { get; set; }
        public int Liked { get; set; }
        public virtual Post Post { get; set; }
        public Guid PostId {get; set;}
        public DateTime Date { get; set; }
         public virtual ICollection<UserComment> UserComments { get; set; }
        public bool IsActive { get; set; }
        
    }
}