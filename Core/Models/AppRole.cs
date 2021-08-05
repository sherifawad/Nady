using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class AppRole : IdentityRole<string>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }

        public AppRole()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}