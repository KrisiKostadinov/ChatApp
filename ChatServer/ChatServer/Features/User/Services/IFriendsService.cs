using ChatServer.Common.Extentions;
using ChatServer.Data.Models.User;
using ChatServer.Features.User.Models.Friend;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatServer.Features.User.Services
{
    public interface IFriendsService
    {
        Task<Result> AddAsync(Friend friend);

        Task<IEnumerable<FriendResponseModel>> GetAllById(string userId);

        Task<Result> RemoveAsync(string userId, string currentUserId);
    }
}
