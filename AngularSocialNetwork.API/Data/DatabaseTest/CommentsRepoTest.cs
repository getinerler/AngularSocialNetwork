using AngularSocialNetwork.API.Models;

namespace AngularSocialNetwork.API.Data.DatabaseTest
{
    public class CommentsRepoTest : ICommentsRepo
    {
        public List<Comment> GetComments(int postId)
        {
            return DatabaseContextTest
                .Comments.Where(x => x.PostId == postId)
                .ToList();
        }
    }
}