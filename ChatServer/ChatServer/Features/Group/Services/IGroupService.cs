using ChatServer.Common.Extentions;
using System.Threading.Tasks;

namespace ChatServer.Features.Group.Services
{
    using ChatServer.Data.Models.Group;

    public interface IGroupService
    {
        Task<Result> Add(Group group);
    }
}
