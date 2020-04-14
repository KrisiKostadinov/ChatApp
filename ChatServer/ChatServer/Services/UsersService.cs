using ChatServer.Data;
using ChatServer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatServer.Services
{
    public class UsersService : IUserService
    {
        private readonly ChatContext context;

        public UsersService(ChatContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await this.context.Users.ToListAsync();
        }
    }
}
