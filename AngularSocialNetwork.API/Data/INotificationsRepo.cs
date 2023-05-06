using AngularSocialNetwork.API.Models;

namespace AngularSocialNetwork.API.Data
{
    public interface INotificationsRepo
    {
        List<Notification> GetNotifications(int userId);
    }
}