﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ChatServer.Data.Models.User
{
    using Data.Models.Group;

    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Groups = new HashSet<Group>();
            UsersGroups = new HashSet<UserGroup>();
        }

        public IEnumerable<Group> Groups { get; set; }

        public IEnumerable<UserGroup> UsersGroups { get; set; }
    }
}
