using AutoMapper;
using ChatServer.Data.Models;
using ChatServer.Data.Models.User;
using ChatServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatServer.Controllers
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
        public async Task<IEnumerable<UserViewModel>> GetAllUsers()
        {
            var users = await this.userService.GetAllUsersAsync();
            return users;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<UserViewModel> GetAllUsers(string id)
        {
            var user = await this.userService.ById(id);
            return user;
        }

        [HttpPost]
        [Route("edit/{id}")]
        public async Task<ActionResult<string>> Edit(string id, UserViewModel model)
        {
            var userDb = await this.userManager.FindByIdAsync(id);

            if (userDb == null)
            {
                return NotFound();
            }
            var user = this.mapper.Map<ApplicationUser>(model);

            var result = await this.userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return user.Id;
            }

            return BadRequest(result.Errors);
        }
    }
}
