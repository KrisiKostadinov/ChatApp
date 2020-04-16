using ChatServer.Data;
using ChatServer.Data.Models;
using ChatServer.Data.Models.User;
using ChatServer.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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

        public async Task<UserViewModel> ById(string id)
        {
            var user = await this.context
                .Users
                .Where(u => u.Id == id)
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    UserName = u.UserName,
                    City = u.City,
                    Age = u.Age,
                    Country = u.Country,
                    Education = u.Education,
                    HighSchool = u.HighSchool,
                    Job = u.Job,
                    PreviousJob = u.PreviousJob,
                    University = u.University,
                })
                .FirstOrDefaultAsync();

            return user;
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

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            var users = await this.context
                .Users
                .Select(u => new UserViewModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    UserName = u.UserName,
                    City = u.City,
                    Age = u.Age,
                    Country = u.Country,
                    Education = u.Education,
                    HighSchool = u.HighSchool,
                    Job = u.Job,
                    PreviousJob = u.PreviousJob,
                    University = u.University,
                    Skills = u.Skills
                }).ToListAsync();

            return users;
        }
    }
}
