using AngularSocialNetwork.API.Models;

namespace AngularSocialNetwork.API.Data.DatabaseTest
{
    public class SeedRepoTest : ISeedRepo
    {
        public void AddFeeds(List<Feed> feeds)
        {
            DatabaseContextTest.Feeds.AddRange(feeds);
        }

        public void AddFollowers(List<Follower> followers)
        {
            DatabaseContextTest.Followers.AddRange(followers);
        }

        public void AddNotifications(List<Notification> notifications)
        {
            DatabaseContextTest.Notifications.AddRange(notifications);
        }

        public void AddPosts(List<Post> posts)
        {
            DatabaseContextTest.Posts.AddRange(posts);
        }

        public void AddUsers(List<User> users)
        {
            DatabaseContextTest.Users.AddRange(users);
        }

        public List<Follower> GetFollowers()
        {
            return DatabaseContextTest.Followers.ToList();
        }

        public List<Post> GetPosts()
        {
            return DatabaseContextTest.Posts.ToList();
        }

        public int GetUserCount()
        {
            return DatabaseContextTest.Users.Count();
        }

        public List<User> GetUsers()
        {
            return DatabaseContextTest.Users.ToList();
        }
    }
}