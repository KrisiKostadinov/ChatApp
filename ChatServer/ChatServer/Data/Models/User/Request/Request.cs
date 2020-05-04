using ChatServer.Data.Models.User;
using System.ComponentModel.DataAnnotations;

namespace ChatServer.Data.Models.User.Request
{
    public class Request
    {
        [Key]
        public string UserIdTo { get; set; }

        public ApplicationUser UserTo { get; set; }

        public string UserIdFrom { get; set; }

        public ApplicationUser UserFrom { get; set; }
    }
}
