using ChatServer.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatServer.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
    }
}
