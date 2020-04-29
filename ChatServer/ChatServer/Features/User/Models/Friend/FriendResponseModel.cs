using AutoMapper;
using ChatServer.Common.Mapping;
using ChatServer.Data.Models.User;

namespace ChatServer.Features.User.Models.Friend
{
    public class FriendResponseModel : IMapFrom<AboutUser>, IHaveCustomMappings
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public int Age { get; set; }

        public string Job { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            var map = configuration.CreateMap<AboutUser, FriendResponseModel>();
            map.ForMember(f => f.Email, f => f.MapFrom(u => u.User.Email));
            map.ForMember(f => f.UserName, f => f.MapFrom(u => u.User.UserName));
            map.ForMember(f => f.Job, f => f.MapFrom(u => u.Job));
        }
    }
}
