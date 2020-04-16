using ChatServer.Data.Models;

namespace ChatServer.Models.User
{
    public class SkillViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }
    }
}
