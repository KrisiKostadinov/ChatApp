using ChatServer.Data.Models;
using ChatServer.Data.Models.Identity;
using ChatServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Controllers
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
                Birthday = model.Birthday,
                UserName = model.UserName
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return StatusCode(201);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<ResponseLoginModel>> Login(LoginRequestModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                return Unauthorized();
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);

            if (passwordValid == null)
            {
                return Unauthorized();
            }

            var token = this.userService.GenerateJWTToken(this.appSettings.Secret, user);
            
            return new ResponseLoginModel
            {
                Token = token,
                Email = user.Email,
                UserName = user.UserName,
            };
        }
    }
}
