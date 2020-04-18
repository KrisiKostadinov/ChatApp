using ChatServer.Controllers;
using ChatServer.Data.Models;
using ChatServer.Data.Models.Identity;
using ChatServer.Data.Models.User;
using ChatServer.Features.User.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ChatServer.Features.Identity.Controllers
{
    public class IdentityController : ApiController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;
        private readonly AppSettings appSettings;

        public IdentityController(
            UserManager<ApplicationUser> userManager,
            IOptions<AppSettings> appSettings,
            IUserService userService)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.UserName
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var aboutUser = new AboutUser
                {
                    UserId = user.Id,
                };

                var userId = await userService.CreateAsync(aboutUser, user.Id);

                return StatusCode(201, userId);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<ResponseLoginModel>> Login(LoginRequestModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return Unauthorized();
            }

            var passwordValid = await userManager.CheckPasswordAsync(user, model.Password);

            if (passwordValid == null)
            {
                return Unauthorized();
            }

            var token = userService.GenerateJWTToken(appSettings.Secret, user);

            return new ResponseLoginModel
            {
                Token = token,
                Email = user.Email,
                UserName = user.UserName,
            };
        }
    }
}
