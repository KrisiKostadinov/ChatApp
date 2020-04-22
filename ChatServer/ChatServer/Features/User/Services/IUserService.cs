using ChatServer.Data.Models.User;
using ChatServer.Features.User.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatServer.Features.User.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUserResponseModel>> GetAllUsersAsync();

        string GenerateJWTToken(string secret, ApplicationUser user);

        Task<AboutUserRequestModel> ById(string id);

        Task<string> UpdateAsync(AboutUser model);

        Task<string> CreateAsync(AboutUser model, string userId);
    }
}
