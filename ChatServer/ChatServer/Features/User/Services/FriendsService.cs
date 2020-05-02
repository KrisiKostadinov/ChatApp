using ChatServer.Common;
using ChatServer.Common.Extentions;
using ChatServer.Common.Mapping;
using ChatServer.Data;
using ChatServer.Data.Models.Group;
using ChatServer.Data.Models.User;
using ChatServer.Data.Models.User.Request;
using ChatServer.Features.User.Models.Friend;
using ChatServer.Features.User.Models.Request;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Features.User.Services
{
    public class FriendsService : IFriendsService
    {
        private readonly ChatContext context;

        public FriendsService(
            ChatContext context)
        {
            this.context = context;
        }

        public async Task<Result> AddAsync(Friend friend)
        {
            if (friend.CurrentUserId == null || friend.OtherUserId == null)
            {
                return Result.Failed(
                    new Error("null", $"{friend.CurrentUserId} or {friend.OtherUserId} not be null."));
            }

            if (friend.CurrentUserId == friend.OtherUserId)
            {
                return Result.Failed(
                    new Error("Dublicate Ids", $"The first and second user they must be different."));
            }

            var checkForFriends = await CheckForFriends(friend.CurrentUserId, friend.OtherUserId);

            if (checkForFriends != null)
            {
                return Result.Failed(
                    new Error("Invalid operation", $"These ids are friends."));
            }

            this.context.Friends.Add(friend);
            await this.context.SaveChangesAsync();
            return Result.Success;
        }

        private async Task<Friend> CheckForFriends(string currentUserId, string otherUserId)
        {
            return await this.context.Friends
                .Where(
                    u => u.CurrentUserId == currentUserId &&
                    u.OtherUserId == otherUserId ||
                    u.OtherUserId == currentUserId &&
                    u.CurrentUserId == otherUserId)
                .FirstOrDefaultAsync();
        }

        public async Task<Result> AddRequest(Request request)
        {
            if (request.UserId == null || request.UserIdFrom == null)
            {
                return Result.Failed(
                    new Error("Invalid operation", $"The ids not be null."));
            }

            var isContans = await this.context
                .Requests
                .Where(r => r.UserIdFrom == request.UserIdFrom && r.UserId == request.UserId)
                .FirstOrDefaultAsync();

            var isFriends = await CheckForFriends(request.UserIdFrom, request.UserId);

            if (isFriends != null)
            {
                return Result.Failed(
                    new Error("Invalid operation", $"These users is friends."));
            }

            if (isContans != null)
            {
                return Result.Failed(
                    new Error("Invalid operation", $"These ids is exists."));
            }

            await this.context.Requests.AddAsync(request);
            await this.context.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<IEnumerable<FriendResponseModel>> GetAllById(string userId)
        {
            var friendIds = from f in this.context
                .Friends
                          where f.CurrentUserId == userId || f.OtherUserId == userId
                          select f.CurrentUserId != userId ? f.CurrentUserId : f.OtherUserId;

            var friends = new List<FriendResponseModel>();

            foreach (var id in friendIds)
            {
                var friend = this.context
                    .AboutUsers
                    .Where(u => u.UserId == id)
                    .To<FriendResponseModel>()
                    .FirstOrDefault();

                friends.Add(friend);
            }

            return friends;
        }

        public async Task<Result> RemoveAsync(string userId, string currentUserId)
        {
            if (userId == null || currentUserId == null)
            {
                return Result.Failed(
                    new Error("Invalid operation", $"The id not be null."));
            }

            var friendId = await this.context
                .Friends
                .Where(
                f => f.CurrentUserId == userId ||
                f.OtherUserId == userId &&
                f.CurrentUserId == currentUserId ||
                f.OtherUserId == userId)
                .FirstOrDefaultAsync();

            if (friendId == null)
            {
                return Result.Failed(
                    new Error("Invalid operation", $"These users is not friends."));
            }

            this.context.Friends.Remove(friendId);

            await this.context.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<IEnumerable<RequestResponseModel>> ListAllRequestsByUserId(string currentUserId)
        {
            var requests = await this.context
                .Requests
                .Where(x => x.UserIdFrom == currentUserId)
                .To<RequestResponseModel>()
                .ToListAsync();

            return requests;
        }

        public async Task<Result> AddMessageOfUser(Message message)
        {
            await this.context.Messages.AddAsync(message);
            await this.context.SaveChangesAsync();
            return Result.Success;
        }

        //public async Task<FriendResponseModel> ById(string currentUserId, string userId)
        //{
        //    var friend = await this.context
        //        .Friends
        //        .Where(x => x.OtherUserId == userId ||
        //            x.CurrentUserId == userId &&
        //            x.CurrentUserId == currentUserId ||
        //            x.OtherUserId == currentUserId)
        //        .To<FriendResponseModel>()
        //        .FirstOrDefaultAsync();
        //    return friend;
        //}
    }
}
