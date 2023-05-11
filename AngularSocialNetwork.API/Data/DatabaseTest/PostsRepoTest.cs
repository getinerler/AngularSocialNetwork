using AngularSocialNetwork.API.Dtos.Posts;
using AngularSocialNetwork.API.Helper;
using AngularSocialNetwork.API.Models;

namespace AngularSocialNetwork.API.Data.DatabaseTest
{
    public class PostsRepoTest : IPostsRepo
    {
        public List<PostForFeedDto> GetPosts(int userId)
        {
            List<PostForFeedDto> list =
            (
                from feed in DatabaseContextTest.Feeds
                join post in DatabaseContextTest.Posts on feed.PostId equals post.PostId
                join user in DatabaseContextTest.Users on post.UserId equals user.UserId
                join repostUser in DatabaseContextTest.Users on feed.RepostedUserId equals repostUser.UserId into rue
                from repostUser in rue.DefaultIfEmpty()

                where userId == -1 || feed.UserId == userId

                orderby post.CreatedDate descending

                select new PostForFeedDto()
                {
                    Id = feed.FeedId,
                    PostId = feed.PostId,
                    UserId = post.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Text = post.Text,
                    Photo = UrlCreate.GetPhotoUrl(user.ProfilePhoto),
                    LikeCount = post.LikeCount,
                    RetweetCount = post.RetweetCount,
                    CommentCount = post.CommentCount,
                    IsReposted = repostUser != null,
                    RepostedFirstName = repostUser?.FirstName ?? string.Empty,
                    RepostedLastName = repostUser?.LastName ?? string.Empty,
                    Date = post.CreatedDate,
                    Liked = feed.Liked,
                    Reposted = feed.Reposted
                })
              .Skip(0)
              .Take(10)
              .ToList();

            return list;
        }

        public PostForFeedDto GetPostDetailed(int feedId)
        {
            Feed feed = DatabaseContextTest.Feeds.FirstOrDefault(x => x.FeedId == feedId);
            Post post = DatabaseContextTest.Posts.FirstOrDefault(x => x.PostId == feed.PostId);
            User postUser = DatabaseContextTest.Users.FirstOrDefault(x => x.UserId == post.UserId);

            List<CommentCount> commentCounts = DatabaseContextTest.CommentCounts
                .Where(x => x.PostId == post.PostId && x.UserId == feed.UserId)
                .ToList();

            List<PostForFeedDto> comments =
            (
                from comment in DatabaseContextTest.Comments
                join user in DatabaseContextTest.Users on comment.UserId equals user.UserId

                where comment.PostId == post.PostId

                orderby comment.CreatedDate descending

                select new PostForFeedDto()
                {
                    Id = feed.FeedId,
                    PostId = feed.PostId,
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Text = comment.Text,
                    Photo = UrlCreate.GetPhotoUrl(user.ProfilePhoto),
                    LikeCount = comment.LikeCount,
                    RetweetCount = comment.RetweetCount,
                    CommentCount = comment.CommentCount,
                    Date = comment.CreatedDate,
                    Liked = commentCounts?.FirstOrDefault(x => x.CommentId == comment.CommentId)?.Liked ?? false,
                    Reposted = commentCounts?.FirstOrDefault(x => x.CommentId == comment.CommentId)?.Reposted ?? false,
                })
            .ToList();

            PostForFeedDto detailedPost = new PostForFeedDto()
            {
                Id = post.PostId,
                UserId = post.UserId,
                FirstName = postUser.FirstName,
                LastName = postUser.LastName,
                Username = postUser.Username,
                Text = post.Text,
                Photo = UrlCreate.GetPhotoUrl(postUser.ProfilePhoto),
                LikeCount = post.LikeCount,
                RetweetCount = post.RetweetCount,
                CommentCount = post.CommentCount,
                Date = post.CreatedDate,
                Liked = feed.Liked,
                Comments = comments
            };

            return detailedPost;
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
                    Liked = false,
                    Reposted = false
                };
                DatabaseContextTest.Feeds.Add(feed);
            }

            Feed userFeed = new Feed()
            {
                FeedId = DatabaseContextTest.Feeds.Max(x => x.FeedId) + 1,
                UserId = postAddDto.Id,
                PostId = post.PostId,
                Liked = false,
                Reposted = false
            };
            DatabaseContextTest.Feeds.Add(userFeed);
        }

        public int LikePost(PostLikeDto req)
        {
            Feed feed = DatabaseContextTest.Feeds.FirstOrDefault(x =>
                x.FeedId == req.FeedId &&
                x.UserId == req.UserId);
            if (feed == null)
            {
                throw new Exception("No feed found.");
            }

            Post post = DatabaseContextTest.Posts.FirstOrDefault(x => x.PostId == feed.PostId);
            if (post == null)
            {
                throw new Exception("No post found.");
            }

            if (feed.Liked)
            {
                post.LikeCount--;
                feed.Liked = false;
            }
            else
            {
                post.LikeCount++;
                feed.Liked = true;
            }

            return post.LikeCount;
        }

        public int RepostPost(PostLikeDto req)
        {
            Feed feed = DatabaseContextTest.Feeds.FirstOrDefault(x =>
                x.FeedId == req.FeedId &&
                x.UserId == req.UserId);
            if (feed == null)
            {
                throw new Exception("No feed found.");
            }

            Post post = DatabaseContextTest.Posts.FirstOrDefault(x => x.PostId == feed.PostId);
            if (post == null)
            {
                throw new Exception("No post found.");
            }


            if (feed.Reposted)
            {
                post.RetweetCount--;

                List<Feed> samePostFeeds = DatabaseContextTest.Feeds
                    .Where(x =>
                        x.PostId == feed.PostId &&
                        x.UserId == req.UserId)
                    .ToList();

                foreach (Feed f in samePostFeeds)
                {
                    f.Reposted = false;
                }

                List<Feed> feedsToRemove = DatabaseContextTest.Feeds
                    .Where(x =>
                        x.PostId == post.PostId &&
                        x.RepostedUserId == req.UserId)
                    .ToList();

                foreach (Feed feedToRemove in feedsToRemove)
                {
                    DatabaseContextTest.Feeds.Remove(feedToRemove);
                }
            }
            else
            {
                post.RetweetCount++;

                List<Feed> samePostFeeds = DatabaseContextTest.Feeds
                    .Where(x =>
                        x.PostId == feed.PostId &&
                        x.UserId == req.UserId)
                    .ToList();

                foreach (Feed f in samePostFeeds)
                {
                    f.Reposted = true;
                }

                List<int> userFollowersIds = DatabaseContextTest.Followers
                   .Where(x => x.FolloweeId == req.UserId)
                   .Select(x => x.FollowerId)
                   .ToList()
                   //Add repost to user's feed too.
                   .Union(new int[] { req.UserId.Value })
                   .ToList();

                foreach (int followerId in userFollowersIds)
                {
                    DatabaseContextTest.Feeds.Add(new Feed()
                    {
                        FeedId = DatabaseContextTest.Feeds.Max(x => x.FeedId) + 1,
                        RepostedUserId = req.UserId.Value,
                        UserId = followerId,
                        PostId = post.PostId,
                        Liked = false,
                        Reposted = false
                    });
                }
            }

            return post.RetweetCount;
        }

        public void DeletePost(int postId)
        {
            Post post = DatabaseContextTest.Posts.FirstOrDefault(x => x.PostId == postId);
            DatabaseContextTest.Posts.Remove(post);
        }
    }
}