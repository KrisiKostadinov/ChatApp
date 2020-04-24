using ChatServer.Features.Group.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatServer.Hubs
{
    [Authorize]
    public class IndexChatHub : Hub
    {
        static HashSet<string> CurrentConnections = new HashSet<string>();
        private readonly IGroupService groupService;

        public IndexChatHub(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        public async Task AddToGroup(string group)
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, group);
            var isInGroup = await this.groupService.IsInGroup(this.Context.UserIdentifier, group);

            if (isInGroup)
            {
                await Clients.All.SendAsync("addedToGroup", this.Context.User.Identity.Name);
            }
        }

        public async Task RemoveFromGroup(string group)
        {
            await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, group);

            var isInGroup = await this.groupService.IsInGroup(this.Context.UserIdentifier, group);

            if (isInGroup)
            {
                await Clients.All.SendAsync("removedFromGroup", this.Context.User.Identity.Name);
            }
        }

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
            CurrentConnections.Add(this.Context.ConnectionId);
            await Clients.Others.SendAsync("UserConnected", this.Context.User.Identity.Name);

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            CurrentConnections.Remove(this.Context.ConnectionId);
            await Clients.Others.SendAsync("UserDisconnected", this.Context.User.Identity.Name);

            await base.OnDisconnectedAsync(ex);
        }
    }
}
