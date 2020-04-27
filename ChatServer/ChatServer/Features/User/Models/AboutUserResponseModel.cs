using ChatServer.Common.Mapping;
using ChatServer.Data.Models.User;

namespace ChatServer.Features.User.Models
{
    public class AboutUserResponseModel : IMapFrom<AboutUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
