using ChatServer.Common.Extentions;
using ChatServer.Data.Models.User;
using System.Threading.Tasks;

namespace ChatServer.Features.User.Services
{
    public interface IFriendsService
    {
        Task<Result> AddAsync(Friend friend);
    }
}
