namespace AngularSocialNetwork.API.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }
        public int LikeCount { get; set; }
        public int RetweetCount { get; set; }
        public int CommentCount { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}