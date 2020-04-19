using AutoMapper;
using ChatServer.Data.Models.User;
using ChatServer.Features.User.Models;

namespace ChatServer.Common.Mapping
{
    public class HaveCustomMappings : IHaveCustomMappings
    {
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<AboutUser, AboutUserResponseModel>();
        }
    }
}
