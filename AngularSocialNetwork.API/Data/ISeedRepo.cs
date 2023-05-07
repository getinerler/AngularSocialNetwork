using AngularSocialNetwork.API.Models;

namespace AngularSocialNetwork.API.Data
{
    public interface ISeedRepo
    {
        int GetUserCount();
        void AddUsers(List<User> users);
        void AddPosts(List<Post> posts);
        void AddFollowers(List<Follower> followers);
        List<User> GetUsers();
        List<Post> GetPosts();
        void AddFeeds(List<Feed> feeds);
        void AddNotifications(List<Notification> notifications);
        List<Follower> GetFollowers();
        void AddComments(List<Comment> comments);
    }
}