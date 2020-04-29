using ChatServer.Common.Mapping;

namespace ChatServer.Features.Group.Models
{
    using AutoMapper;
    using Data.Models.Group;
    using System.Collections.Generic;

    public class GroupResponseModel : IMapFrom<Group>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public ICollection<UserGroup> Users { get; set; }

        public bool isJoined { get; set; }

        public bool IsMy { get; set; }

        public string OwnerId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            var map = configuration.CreateMap<Group, GroupResponseModel>();
            map.ForMember(x => x.Email, x => x.MapFrom(x => x.User.Email));
            map.ForMember(x => x.UserName, x => x.MapFrom(x => x.User.UserName));
            map.ForMember(x => x.Users, x => x.MapFrom(x => x.UsersGroups));
        }
    }
}
