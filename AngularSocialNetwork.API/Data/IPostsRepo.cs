using AngularSocialNetwork.API.Dtos.Posts;

namespace AngularSocialNetwork.API.Data
{
    public interface IPostsRepo
    {
        List<PostForFeedDto> GetPosts(int userId);
        PostForFeedDto GetPostDetailed(int feedId);
        void SaveNewPost(PostAddDto postAddDto);
        int LikePost(PostLikeDto req);
        int RepostPost(PostLikeDto req);
        void DeletePost(int postId);
    }
}