using ChatServer.Common.Extentions;
using System.Threading.Tasks;

namespace ChatServer.Features.Group.Services
{
    using ChatServer.Data.Models.Group;
    using ChatServer.Features.Group.Models;
    using ChatServer.Features.User.Models;
    using System.Collections.Generic;

    public interface IGroupService
    {
        Task<Result> Add(Group group);

        Task<IEnumerable<GroupResponseModel>> AllByUserId(string userId);

        Task<GroupResponseModel> ById(int id);

        Task<Result> EditAsync(int id, Group group);

        Task<Result> Dismiss(int id);

        Task<Result> AddToGroup(int groupId, string userId);

        Task<bool> IsInGroup(string userId, string group);

        Task<Result> RemoveFromGroup(int groupId, string userId);

        Task<IEnumerable<AboutUserResponseModel>> AllInGroup(int groupId);

        Task<IEnumerable<GroupResponseModel>> ListAll();
    }
}
