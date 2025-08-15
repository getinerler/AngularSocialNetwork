namespace AngularSocialNetwork.API.Hubs
{
    public interface IAngularClient
    {
        Task NewNotification(int value);
    }
}