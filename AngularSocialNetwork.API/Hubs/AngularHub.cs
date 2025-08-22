using Microsoft.AspNetCore.SignalR;
using System.Collections;

namespace AngularSocialNetwork.API.Hubs
{
    public class AngularHub : Hub<IAngularClient>
    {
        public static Hashtable UserConnectionIds { get; set; } = new Hashtable{ };
        public AngularHub()
        {

        }

        public async Task SendMessage(int userId, int number)
        {
            string connectionId = UserConnectionIds[userId].ToString();
            if (connectionId == null)
            {
                return;
            }
            await Clients.All.NewNotification(number);
        }
        
        public override async Task OnConnectedAsync()
        {
            int userId = Convert.ToInt32(Context.GetHttpContext().Request?.Query["userId"]);
            if (UserConnectionIds.ContainsKey(userId))
            {
                UserConnectionIds[userId] = Context.ConnectionId;
            }
            else
            {
                UserConnectionIds.Add(userId, Context.ConnectionId);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await base.OnDisconnectedAsync(ex);
        }
    }
}