using ChatServer.Data.Models.User;

namespace ChatServer.Data.Models.Group
{
    public class Message
    {
        public int Id { get; set; }

        public int GroupId { get; set; }

        public Group Subject { get; set; }

        public string Content { get; set; }

        public string SenderId { get; set; }

        public ApplicationUser Sender { get; set; }

        public string ReceiverId { get; set; }

        public ApplicationUser Receiver { get; set; }
    }
}
