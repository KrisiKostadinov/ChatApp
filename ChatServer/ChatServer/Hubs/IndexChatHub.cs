using ChatServer.Data.Models.Group;
using ChatServer.Features.User.Models.Friend;
using ChatServer.Features.User.Services;
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
        static HashSet<DbSetMessageModel> CurrentConnections = new HashSet<DbSetMessageModel>();
        private readonly IFriendsService friendsService;

        public IndexChatHub(IFriendsService friendsService)
        {
            this.friendsService = friendsService;
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
            var receiver = CurrentConnections
                .Where(x => x.ConnectionId == connectionId)
                .FirstOrDefault();
            var sender = CurrentConnections
                .Where(x => x.ConnectionId == this.Context.ConnectionId)
                .FirstOrDefault();

            if (connectionId != null)
            {
                await Clients
                    .Client(connectionId)
                    .SendAsync("ReceiveMsg", receiver.UserName, receiver.UserId, sender.UserName, sender.UserId, content);
                await Clients
                    .Caller
                    .SendAsync("ReceiveMsg", receiver.UserName, receiver.UserId, sender.UserName, sender.UserId, content);
            }

            var messageModel = new Message
            {
                Content = content,
                ReceiverId = receiver.UserId,
                SenderId = sender.UserId,
            };

            await this.friendsService.AddMessageOfUser(messageModel);
        }

        public Task SendMessageToAll(string msg)
        {
            return Clients.All.SendAsync("ReceiveMsg", msg);
        }

        public override async Task OnConnectedAsync()
        {
            var model = new DbSetMessageModel
            {
                ConnectionId = this.Context.ConnectionId,
                UserId = this.Context.UserIdentifier,
                UserName = this.Context.User.Identity.Name,
            };

            CurrentConnections.Add(model);

            var otherUser = CurrentConnections.Where(x => x.UserId != this.Context.UserIdentifier).FirstOrDefault();
            var currentUser = CurrentConnections.Where(x => x.UserId == this.Context.UserIdentifier).FirstOrDefault();

            if (otherUser != null && currentUser != null)
            {
                await Clients.Caller.SendAsync("UserConnected", otherUser.ConnectionId, otherUser.UserId);
                await Clients.Client(otherUser.ConnectionId).SendAsync("UserConnected", currentUser.ConnectionId, currentUser.UserId);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            CurrentConnections.RemoveWhere(x => x.ConnectionId == this.Context.ConnectionId);
            await Clients.Others.SendAsync("UserDisconnected", this.Context.UserIdentifier);

            await base.OnDisconnectedAsync(ex);
        }
    }
}
