namespace AngularSocialNetwork.API.Models
{
    public class Follower
    {
        public int FollowerId { get; set; }
        public int FolloweeId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}