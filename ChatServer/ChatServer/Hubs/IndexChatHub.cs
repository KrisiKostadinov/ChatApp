using ChatServer.Features.Group.Models;
using ChatServer.Features.Group.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Hubs
{
    [Authorize]
    public class IndexChatHub : Hub
    {
        static HashSet<MessageResponseModel> CurrentConnections = new HashSet<MessageResponseModel>();
        private readonly IGroupService groupService;

        public IndexChatHub(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        //public async Task AddToGroup(string group)
        //{
        //    await this.Groups.AddToGroupAsync(this.Context.ConnectionId, group);
        //    var isInGroup = await this.groupService.IsInGroup(this.Context.UserIdentifier, group);

        //    if (isInGroup)
        //    {
        //        await Clients.All.SendAsync("addedToGroup", this.Context.User.Identity.Name);
        //    }
        //}

        //public async Task RemoveFromGroup(string group)
        //{
        //    await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, group);

        //    var isInGroup = await this.groupService.IsInGroup(this.Context.UserIdentifier, group);

        //    if (isInGroup)
        //    {
        //        await Clients.All.SendAsync("removedFromGroup", this.Context.User.Identity.Name);
        //    }
        //}

        //public Task SendMessageToAll(string msg)
        //{
        //    return Clients.All.SendAsync("ReceiveMsg", msg);
        //}

        public Task SendMessageToCaller(string msg)
        {
            return Clients.Caller.SendAsync("ReceiveMsg", msg);
        }

        public async Task SendMessageToUser(string connectionId, string content)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMsg", this.Context.User.Identity.Name, content);
            await Clients.Caller.SendAsync("ReceiveMsg", this.Context.User.Identity.Name, content);
        }

        public Task SendMessageToAll(string msg)
        {
            return Clients.All.SendAsync("ReceiveMsg", msg);
        }

        public override async Task OnConnectedAsync()
        {
            var model = new MessageResponseModel
            {
                ConnectionId = this.Context.ConnectionId,
                UserName = this.Context.User.Identity.Name,
            };

            CurrentConnections.Add(model);

            var otherUser = CurrentConnections.Where(x => x.UserName != this.Context.User.Identity.Name).FirstOrDefault();
            var currentUser = CurrentConnections.Where(x => x.UserName == this.Context.User.Identity.Name).FirstOrDefault();

            if (otherUser != null)
            {
                await Clients.Caller.SendAsync("UserConnected", otherUser.ConnectionId, otherUser.UserName);
                await Clients.Client(otherUser.ConnectionId).SendAsync("UserConnected", currentUser.ConnectionId, currentUser.UserName);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            CurrentConnections.RemoveWhere(x => x.ConnectionId == this.Context.ConnectionId);
            await Clients.Others.SendAsync("UserDisconnected", this.Context.User.Identity.Name);

            await base.OnDisconnectedAsync(ex);
        }
    }
}
