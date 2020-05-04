using AutoMapper;
using ChatServer.Common.Mapping;

namespace ChatServer.Features.User.Models.Request
{
    using Data.Models.User.Request;

    public class RequestResponseModel: IMapFrom<Request>, IHaveCustomMappings
    {
        public string UserName { get; set; }

        public string UserId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Request, RequestResponseModel>()
                .ForMember(x => x.UserId, x => x.MapFrom(x => x.UserFrom.Id))
                .ForMember(x => x.UserName, x => x.MapFrom(x => x.UserFrom.UserName));
        }
    }
}
