namespace ChatServer.Data.Models.User
{
    public class Friend
    {
        public int Id { get; set; }

        public string CurrentUserId { get; set; }

        public string OtherUserId { get; set; }
    }
}
