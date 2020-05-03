using AutoMapper;
using ChatServer.Data.Models.Group;
using ChatServer.Data.Models.User;
using ChatServer.Features.User.Models;
using ChatServer.Features.User.Models.Friend;
using ChatServer.Features.User.Models.Request;

namespace ChatServer.Common.Mapping
{
    public class HaveCustomMappings : IHaveCustomMappings
    {
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AboutUser, AboutUserResponseModel>();
            configuration.CreateMap<ApplicationUser, AboutUserResponseModel>();
            configuration.CreateMap<Friend, FriendResponseModel>();
            configuration.CreateMap<string, RequestResponseModel>();
            configuration.CreateMap<Message, MessageResponseModel>();
        }
    }
}
