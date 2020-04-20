using ChatServer.Common;
using ChatServer.Common.Extentions;
using ChatServer.Common.Mapping;
using ChatServer.Data;
using ChatServer.Data.Models.User;
using ChatServer.Features.User.Models.Friend;
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

            var checkForFriends = this.context.Friends
                .FirstOrDefault(
                    u => u.CurrentUserId == friend.CurrentUserId &&
                    u.OtherUserId == friend.OtherUserId ||
                    u.OtherUserId == friend.CurrentUserId &&
                    u.CurrentUserId == friend.OtherUserId);

            if (checkForFriends != null)
            {
                return Result.Failed(
                    new Error("Invalid operation", $"These ids are friends."));
            }

            this.context.Friends.Add(friend);
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
    }
}
