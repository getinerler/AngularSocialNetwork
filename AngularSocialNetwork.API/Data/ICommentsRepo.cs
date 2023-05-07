using AngularSocialNetwork.API.Models;

namespace AngularSocialNetwork.API.Data
{
    public interface ICommentsRepo
    {
        List<Comment> GetComments(int postId);
    }
}