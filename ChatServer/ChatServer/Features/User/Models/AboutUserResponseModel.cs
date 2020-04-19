using ChatServer.Common.Mapping;
using ChatServer.Data.Models.User;

namespace ChatServer.Features.User.Models
{
    public class AboutUserResponseModel : IMapFrom<AboutUser>
    {
        public string Id { get; set; }
    }
}
