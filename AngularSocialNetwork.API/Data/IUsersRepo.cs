using AngularSocialNetwork.API.Dtos.Posts;
using AngularSocialNetwork.API.Dtos.Users;

namespace AngularSocialNetwork.API.Data
{
    public interface IUsersRepo
    {
        UserForProfileDto GetProfileInfo(int id, int? userId);
        List<PostForFeedDto> GetUserPosts(int id);
        List<FollowerDto> GetFollowers(int id);
        List<FollowerDto> GetFollowings(int id);
        void Follow(int id, int userId, bool follow);
    }
}