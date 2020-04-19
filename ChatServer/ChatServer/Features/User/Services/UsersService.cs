using ChatServer.Common.Mapping;
using ChatServer.Data;
using ChatServer.Data.Models;
using ChatServer.Data.Models.User;
using ChatServer.Features.User.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Features.User.Services
{
    public class UsersService : IUserService
    {
        private readonly ChatContext context;

        public UsersService(ChatContext context)
        {
            this.context = context;
        }

        public async Task<AboutUserRequestModel> ById(string id)
        {
            var user = await context
                .AboutUsers
                .Where(u => u.UserId == id)
                .To<AboutUserRequestModel>()
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<string> CreateAsync(AboutUser model, string userId)
        {
            context.AboutUsers.Add(model);
            await context.SaveChangesAsync();

            return userId;
        }

        public async Task<string> UpdateAsync(AboutUser model)
        {
            context.AboutUsers.Update(model);
            await context.SaveChangesAsync();
            return model.UserId;
        }

        public string GenerateJWTToken(string secret, ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encriptedToken = tokenHandler.WriteToken(token);
            return encriptedToken;
        }

        public async Task<IEnumerable<ApplicationUserResponseModel>> GetAllUsersAsync()
        {
            var users = await context
                .AboutUsers
                .To<ApplicationUserResponseModel>()
                .ToListAsync();

            return users;
        }
    }
}
