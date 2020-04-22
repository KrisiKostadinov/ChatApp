using AutoMapper;
using ChatServer.Data.Models.Group;
using ChatServer.Data.Models.User;
using ChatServer.Features.Group.Models;
using ChatServer.Features.User.Models;

namespace ChatServer.Data.Extentions
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            this.CreateMap<AboutUser, AboutUserRequestModel>();
            this.CreateMap<AboutUserRequestModel, AboutUser>();
            this.CreateMap<GroupRequestModel, Group>();
            this.CreateMap<GroupRequestModel, GroupResponseModel>();
        }
    }
}
