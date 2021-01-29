using System;

namespace Application.Helpers
{
    public class Params
    {
        private const int MaxPageSize = 20; 
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 3;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize =(value > MaxPageSize ? MaxPageSize: value);
        }
        public Guid Category { get; set; }
    }
}