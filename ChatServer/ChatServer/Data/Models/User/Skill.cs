namespace ChatServer.Data.Models.User
{
    public class Skill
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }
    }
}
