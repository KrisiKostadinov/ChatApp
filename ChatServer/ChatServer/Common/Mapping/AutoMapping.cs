using AutoMapper;
using ChatServer.Data.Models;
using ChatServer.Data.Models.User;

namespace ChatServer.Data.Extentions
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            this.CreateMap<UserViewModel, ApplicationUser>();
        }
    }
}
