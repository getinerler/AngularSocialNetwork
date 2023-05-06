using AngularSocialNetwork.API.Dtos.Posts;

namespace AngularSocialNetwork.API.Data
{
    public interface IPostsRepo
    {
        List<PostForFeedDto> GetPosts(int userId);
        void SaveNewPost(PostAddDto postAddDto);
    }
}