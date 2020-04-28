using AutoMapper;
using ChatServer.Controllers;
using ChatServer.Data.Models.User;
using ChatServer.Features.User.Models;
using ChatServer.Features.User.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatServer.Features.User.Controllers
{
    [Authorize]
    public class UsersController : ApiController
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UsersController(
            IUserService userService,
            IMapper mapper)
        {
            this.userService = userService;
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
        public async Task<ApplicationUserResponseModel> ById(string id)
        {
            var user = await userService.ById(id);
            return user;
        }

        [HttpPut]
        public async Task<ActionResult<string>> UpdateAsync(AboutUserRequestModel model)
        {
            var aboutUserDb = await userService.ById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (aboutUserDb == null)
            {
                return NotFound();
            }

            var aboutUser = mapper.Map<AboutUser>(model);
            aboutUser.Id = aboutUserDb.Id;
            aboutUser.UserId = currentUserId;

            await userService.UpdateAsync(aboutUser);
            return currentUserId;
        }
    }
}
