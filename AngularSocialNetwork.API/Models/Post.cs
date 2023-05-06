namespace AngularSocialNetwork.API.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public int LikeCount { get; set; } = 0;
        public int RetweetCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}