using AutoMapper;
using ChatServer.Controllers;
using ChatServer.Data.Models;
using ChatServer.Data.Models.User;
using ChatServer.Features.Identity.Services;
using ChatServer.Features.User.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatServer.Features.User.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IMapper mapper;

        public UsersController(
            IUserService userService,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            this.userService = userService;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<ApplicationUserResponseModel>> GetAllUsers()
        {
            var users = await userService.GetAllUsersAsync();
            return users;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<AboutUserRequestModel> GetAllUsers(string id)
        {
            var user = await userService.ById(id);
            return user;
        }

        [HttpPost]
        [Route("edit/{userId}")]
        public async Task<ActionResult<string>> UpdateAsync(string userId, AboutUserRequestModel model)
        {
            var aboutUserDb = await userService.ById(userId);
            if (aboutUserDb == null)
            {
                return NotFound();
            }

            var aboutUser = mapper.Map<AboutUser>(model);
            aboutUser.Id = aboutUserDb.Id;
            aboutUser.UserId = userId;

            await userService.UpdateAsync(aboutUser);
            return userId;
        }
    }
}
