using AutoMapper;
using ChatServer.Common.Mapping;
using ChatServer.Data.Models.User;

namespace ChatServer.Features.User.Models
{
    public class ApplicationUserResponseModel : IMapFrom<AboutUser>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int Age { get; set; }

        public string Job { get; set; }

        public string PreviousJob { get; set; }

        public string Education { get; set; }

        public string Skills { get; set; }

        public string HighSchool { get; set; }

        public int University { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            var map = configuration.CreateMap<AboutUser, ApplicationUserResponseModel>();
            map.ForMember(x => x.UserName, x => x.MapFrom(x => x.User.UserName));
            map.ForMember(x => x.Email, x => x.MapFrom(x => x.User.Email));
        }
    }
}
