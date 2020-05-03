using AutoMapper;
using ChatServer.Common.Mapping;
using ChatServer.Data.Models.Group;

namespace ChatServer.Features.User.Models.Friend
{
    public class MessageResponseModel : IMapFrom<Message>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string SenderUserName { get; set; }

        public string SenderId { get; set; }

        public string ReceiverUserName { get; set; }

        public string ReceiverId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Message, MessageResponseModel>()
                .ForMember(x => x.SenderUserName, x => x.MapFrom(x => x.Sender.UserName))
                .ForMember(x => x.ReceiverUserName, x => x.MapFrom(x => x.Receiver.UserName));
        }
    }
}
