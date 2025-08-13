using AngularSocialNetwork.API.Dtos.Posts;
using AngularSocialNetwork.API.Dtos.Users;
using AngularSocialNetwork.API.Helper;
using AngularSocialNetwork.API.Models;

namespace AngularSocialNetwork.API.Data.DatabaseTest
{
    public class UsersRepoTest : IUsersRepo
    {
        public UserForProfileDto GetProfileInfo(int id, int? userId)
        {
            User user = DatabaseContextTest.Users.FirstOrDefault(x => x.UserId == id);
            if (user == null)
            {
                throw new Exception("No user with id: " + userId);
            }

            UserForProfileDto userForProfile = new UserForProfileDto()
            {
                Id = user.UserId,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Bio = user.Bio,
                Link = user.ProfileLink,
                Photo = UrlCreate.GetPhotoUrl(user.ProfilePhoto),
                Date = user.CreatedDate,
                FollowerCount = user.FollowerCount,
                FollowingCount = user.FollowingCount
            };

            if (userId.HasValue)
            {
                Follower follow = DatabaseContextTest.Followers
                    .FirstOrDefault(x => x.FollowerId == userId.Value && x.FolloweeId == id);

                if (follow != null)
                {
                    userForProfile.Following = true;
                }
            }
            return userForProfile;
        }

        public List<PostForFeedDto> GetUserPosts(int id)
        {
            List<PostForFeedDto> list = (
                from feed in DatabaseContextTest.Feeds
                join post in DatabaseContextTest.Posts on feed.PostId equals post.PostId
                join user in DatabaseContextTest.Users on post.UserId equals user.UserId

                where post.UserId == id
                where feed.UserId == id

                orderby post.CreatedDate descending

                select new PostForFeedDto()
                {
                    Id = feed.FeedId,
                    UserId = post.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Text = post.Text,
                    Photo = UrlCreate.GetPhotoUrl(user.ProfilePhoto),
                    LikeCount = post.LikeCount,
                    RetweetCount = post.RetweetCount,
                    CommentCount = post.CommentCount,
                    Date = post.CreatedDate,
                    Liked = feed.Liked,
                    Reposted = feed.Reposted
                })
                .Skip(0)
                .Take(10)
                .ToList();

            return list;
        }

        public List<FollowerDto> GetFollowers(int id)
        {
            List<FollowerDto> followers =
            (
                from follower in DatabaseContextTest.Followers
                join user in DatabaseContextTest.Users on follower.FollowerId equals user.UserId

                where follower.FolloweeId == id

                select new FollowerDto()
                {
                    Id = user.UserId,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Photo = UrlCreate.GetPhotoUrl(user.ProfilePhoto),
                })
            .ToList();

            return followers;
        }

        public List<FollowerDto> GetFollowings(int id)
        {
            List<FollowerDto> followers =
            (
                from follower in DatabaseContextTest.Followers
                join user in DatabaseContextTest.Users on follower.FolloweeId equals user.UserId

                where follower.FollowerId == id

                select new FollowerDto()
                {
                    Id = user.UserId,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Photo = UrlCreate.GetPhotoUrl(user.ProfilePhoto),
                })
            .ToList();

            return followers;
        }

        public void Follow (int id, int userId, bool follow)
        {
            Follower follower = DatabaseContextTest.Followers
                .FirstOrDefault(x => x.FollowerId == id && x.FolloweeId == userId);
            if (follow && follower == null)
            {
                Follower newFollower = new Follower()
                {
                    FollowerId = id,
                    FolloweeId = userId
                };
                DatabaseContextTest.Followers.Add(newFollower);          
            }
            else if (!follow && follower != null)
            {
                DatabaseContextTest.Followers.Remove(follower);
            }
        }
    }
}