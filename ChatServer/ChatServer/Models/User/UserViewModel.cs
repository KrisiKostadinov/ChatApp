using ChatServer.Models.User;
using System.Collections.Generic;

namespace ChatServer.Data.Models.User
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            this.Skills = new List<SkillViewModel>();
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int Age { get; set; }

        public string Job { get; set; }

        public string PreviousJob { get; set; }

        public string Education { get; set; }

        public virtual IEnumerable<SkillViewModel> Skills { get; set; }

        public string HighSchool { get; set; }

        public int University { get; set; }
    }
}
