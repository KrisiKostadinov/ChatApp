namespace ChatServer.Features.User.Models
{
    public class AboutUserRequestModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int Age { get; set; }

        public string Job { get; set; }

        public string PreviousJob { get; set; }

        public string Education { get; set; }

        public string Skills { get; set; }

        public string HighSchool { get; set; }

        public int University { get; set; }
    }
}
