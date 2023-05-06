using AngularSocialNetwork.API.Dtos.Posts;
using AngularSocialNetwork.API.Dtos.Users;

namespace AngularSocialNetwork.API.Data
{
    public interface IUsersRepo
    {
        UserForProfileDto GetProfileInfo(int userId);
        List<PostForFeedDto> GetUserPosts(int userId);
    }
}