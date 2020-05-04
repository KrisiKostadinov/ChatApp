using AutoMapper;
using ChatServer.Common.Mapping;

namespace ChatServer.Features.User.Models.Request
{
    using Data.Models.User.Request;

    public class RequestResponseModel: IMapFrom<Request>, IHaveCustomMappings
    {
        public string UserNameFrom { get; set; }

        public string UserIdFrom { get; set; }

        public string UserNameTo { get; set; }

        public string UserIdTo { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Request, RequestResponseModel>()
                .ForMember(x => x.UserIdFrom, x => x.MapFrom(x => x.UserFrom.Id))
                .ForMember(x => x.UserNameFrom, x => x.MapFrom(x => x.UserFrom.UserName))
                .ForMember(x => x.UserIdTo, x => x.MapFrom(x => x.UserTo.Id))
                .ForMember(x => x.UserNameTo, x => x.MapFrom(x => x.UserTo.UserName));
        }
    }
}
