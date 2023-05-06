using AngularSocialNetwork.API.Dtos.Posts;
using AngularSocialNetwork.API.Helper;
using AngularSocialNetwork.API.Models;

namespace AngularSocialNetwork.API.Data.DatabaseTest
{
    public class PostsRepoTest : IPostsRepo
    {
        public List<PostForFeedDto> GetPosts(int userId)
        {
            List<PostForFeedDto> list = (

              from feed in DatabaseContextTest.Feeds
              join post in DatabaseContextTest.Posts on feed.PostId equals post.PostId
              join user in DatabaseContextTest.Users on post.UserId equals user.UserId

              where userId == -1 || feed.UserId == userId

              orderby post.CreatedDate descending

              select new PostForFeedDto()
              {
                  Id = post.PostId,
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
                  Liked = feed.Liked
              })
              .Skip(0)
              .Take(10)
              .ToList();

            return list;
        }

        public void SaveNewPost(PostAddDto postAddDto)
        {
            Post post = new Post()
            {
                PostId = DatabaseContextTest.Posts.Max(x => x.PostId) + 1,
                UserId = postAddDto.Id,
                Text = postAddDto.Text,
                LikeCount = 0,
                RetweetCount = 0,
                CommentCount = 0,
                CreatedDate = DateTime.Now
            };

            DatabaseContextTest.Posts.Add(post);

            List<Follower> userFollowers = DatabaseContextTest.Followers
                .Where(x => x.FolloweeId == postAddDto.Id)
                .ToList();

            foreach (Follower follower in userFollowers)
            {
                Feed feed = new Feed()
                {
                    FeedId = DatabaseContextTest.Feeds.Max(x => x.FeedId) + 1,
                    UserId = follower.FollowerId,
                    PostId = post.PostId,
                    Liked = false
                };
                DatabaseContextTest.Feeds.Add(feed);
            }

            Feed userFeed = new Feed()
            {
                FeedId = DatabaseContextTest.Feeds.Max(x => x.FeedId) + 1,
                UserId = postAddDto.Id,
                PostId = post.PostId,
                Liked = false
            };
            DatabaseContextTest.Feeds.Add(userFeed);
        }
    }
}