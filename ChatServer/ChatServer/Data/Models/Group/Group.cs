using ChatServer.Data.Models.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatServer.Data.Models.Group
{
    public class Group
    {
        public int Id { get; set; }

        [Required]
        public string Subject { get; set; }

        public string Description { get; set; }

        public ICollection<UserGroup> UsersGroups { get; set; }

        public string OwnerId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
