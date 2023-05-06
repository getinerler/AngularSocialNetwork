using AngularSocialNetwork.API.Models;

namespace AngularSocialNetwork.API.Data.DatabaseTest
{
    public class DatabaseContextTest
    {
        public static List<Comment> Comments { get; set; } = new List<Comment>();
        public static List<Conversation> Conversations { get; set; } = new List<Conversation>();
        public static List<Feed> Feeds { get; set; } = new List<Feed>();
        public static List<Follower> Followers { get; set; } = new List<Follower>();
        public static List<Message> Messages { get; set; } = new List<Message>();
        public static List<Notification> Notifications { get; set; } = new List<Notification>();
        public static List<Post> Posts { get; set; } = new List<Post>();
        public static List<User> Users { get; set; } = new List<User>();
    }
}