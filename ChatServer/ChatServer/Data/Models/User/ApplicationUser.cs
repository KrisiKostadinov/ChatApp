using Microsoft.AspNetCore.Identity;
using System;

namespace ChatServer.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime Birthday { get; set; }
    }
}
