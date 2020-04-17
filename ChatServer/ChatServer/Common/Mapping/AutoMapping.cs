using AutoMapper;
using ChatServer.Data.Models.User;
using ChatServer.Features.User.Models;

namespace ChatServer.Data.Extentions
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            this.CreateMap<AboutUserRequestModel, AboutUser>();
        }
    }
}
