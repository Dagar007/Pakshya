using System;
using System.Collections.Generic;

namespace Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<UserInterest> UserInterests { get; set; }
    }
}