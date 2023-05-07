namespace AngularSocialNetwork.API.Models
{
    public class CommentCount
    {
        public int CommentCountId { get; set; }
        public int PostId { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public bool Liked { get; set; }
        public bool Reweeted { get; set; }
    }
}