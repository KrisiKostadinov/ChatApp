using ChatServer.Data.Models.User;

namespace ChatServer.Data.Models.Group
{
    public class Participant
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public Group Group { get; set; }

        public int GroupId { get; set; }
    }
}
