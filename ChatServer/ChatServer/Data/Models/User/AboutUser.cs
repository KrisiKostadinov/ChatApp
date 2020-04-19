using ChatServer.Common.Mapping;
using ChatServer.Features.User.Models;
using System;

namespace ChatServer.Data.Models.User
{
    public class AboutUser : IMapFrom<AboutUserRequestModel>
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public DateTime Birthday { get; set; }

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
