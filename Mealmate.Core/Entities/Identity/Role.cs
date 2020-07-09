using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mealmate.Core.Entities
{
    public class Role : IdentityRole<int>
    {
        public DateTimeOffset Created { get; set; }

        public Role()
        {
            UserRoles = new HashSet<UserRole>();
            RoleClaims = new HashSet<RoleClaim>();
        }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }

    }
}
