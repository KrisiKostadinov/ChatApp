using ChatServer.Features.Group.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace ChatServer.Hubs
{
    [Authorize]
    public class IndexChatHub : Hub
    {
        public Task SendMessageToAll(string msg)
        {
            return Clients.All.SendAsync("ReceiveMsg", msg);
        }

        public Task SendMessageToCaller(string msg)
        {
            return Clients.Caller.SendAsync("ReceiveMsg", msg);
        }

        public Task SendMessageToUser(string connectionId, string msg)
        {
            return Clients.Client(connectionId).SendAsync("ReceiveMsg", msg);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("UserConnected", this.Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.All.SendAsync("UserDisconnected", this.Context.ConnectionId);
            await base.OnDisconnectedAsync(ex);
        }
    }
}
