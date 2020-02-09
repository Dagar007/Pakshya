namespace Application.Helpers
{
    public class PostParams
    {
        private const int MaxPageSize = 20; 
        public int PageNumber { get; set; } = 1;
        private int pageSize = 3;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize =(value> MaxPageSize ? MaxPageSize: value); }
        }
        public string Category { get; set; }
    }
}