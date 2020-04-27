using ChatServer.Data.Models.User;
using System.ComponentModel.DataAnnotations;

namespace ChatServer.Data.Models.Group
{
    public class UserGroup
    {
        public ApplicationUser User { get; set; }

        public string UserId { get; set; }

        public Group Group { get; set; }

        public int GroupId { get; set; }
    }
}
