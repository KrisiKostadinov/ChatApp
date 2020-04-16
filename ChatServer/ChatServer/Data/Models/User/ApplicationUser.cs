using ChatServer.Data.Models.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ChatServer.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Skills = new List<Skill>();
        }

        public DateTime Birthday { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int Age { get; set; }

        public string Job { get; set; }

        public string PreviousJob { get; set; }

        public string Education { get; set; }

        public IEnumerable<Skill> Skills { get; set; }

        public string HighSchool { get; set; }

        public int University { get; set; }
    }
}
