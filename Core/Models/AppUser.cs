using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class AppUser : IdentityUser<string>
    {
        public string DisplayName { get; set; }
        public DateTimeOffset LastActive { get; set; } = DateTimeOffset.Now;
        public virtual ICollection<AppUserRole> UserRoles { get; set; }

        public AppUser()
        {
            Id = Guid.NewGuid().ToString();
        }

    }
}