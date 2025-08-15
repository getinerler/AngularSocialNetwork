using Microsoft.AspNetCore.SignalR;
using System.Collections;

namespace AngularSocialNetwork.API.Hubs
{
    public class AngularHub : Hub<IAngularClient>
    {
        public Hashtable UserConnectionIds { get; set; } = new Hashtable{ };
        public AngularHub()
        {

        }
        
        public async Task SendMessage(int userId, int number)
        {
            string connectionId = UserConnectionIds[userId.ToString()].ToString();
            await Clients.User(connectionId).NewNotification(number);
        }
        
        public override async Task OnConnectedAsync()
        {
            int userId = Convert.ToInt32(Context.GetHttpContext().Request?.Query["userId"]);
            UserConnectionIds.Add(userId, Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            Console.WriteLine(Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
        }
    }
}