using System;
using System.ComponentModel.DataAnnotations;

namespace ChatServer.Data.Models.Identity
{
    public class RegisterRequestModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        public DateTime Birthday { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
