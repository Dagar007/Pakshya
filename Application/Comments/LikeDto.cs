namespace Application.Comments
{
    public class LikeDto
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public bool IsAuthor { get; set; }
        public int NoOfLikes { get; set; }
    }
}