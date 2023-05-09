using AngularSocialNetwork.API.Dtos.Posts;
using AngularSocialNetwork.API.Dtos.Users;
using AngularSocialNetwork.API.Helper;
using AngularSocialNetwork.API.Models;

namespace AngularSocialNetwork.API.Data.DatabaseTest
{
    public class UsersRepoTest : IUsersRepo
    {
        public UserForProfileDto GetProfileInfo(int userId)
        {
            User user = DatabaseContextTest.Users.FirstOrDefault(x => x.UserId == userId);
            if (user == null)
            {
                throw new Exception("No user with id: " + userId);
            }

            UserForProfileDto userForProfile = new UserForProfileDto()
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Bio = user.Bio,
                Link = user.ProfileLink,
                Photo = UrlCreate.GetPhotoUrl(user.ProfilePhoto),
                Date = user.CreatedDate,
            };

            return userForProfile;
        }

        public List<PostForFeedDto> GetUserPosts(int userId)
        {
            List<PostForFeedDto> list = (
                from feed in DatabaseContextTest.Feeds
                join post in DatabaseContextTest.Posts on feed.PostId equals post.PostId
                join user in DatabaseContextTest.Users on post.UserId equals user.UserId

                where post.UserId == userId
                where feed.UserId == userId

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
    }
}