namespace AngularSocialNetwork.API.Models
{
    public class Feed
    {
        public int FeedId { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public bool Liked { get; set; }
    }
}