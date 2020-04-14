using ChatServer.Data.Models;
using ChatServer.Data.Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatServer.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();

        string GenerateJWTToken(string secret, ApplicationUser user);
    }
}
