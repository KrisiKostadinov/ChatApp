using ChatServer.Data.Models.User;
using ChatServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatServer.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<UserViewModel>> GetAllUsers()
        {
            var users = await this.userService.GetAllUsersAsync();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                userViewModels.Add(new UserViewModel
                {
                    UserName = user.UserName,
                    Email = user.Email
                });
            }

            return userViewModels;
        }
    }
}
