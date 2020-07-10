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
            UserRoles = new HashSet<IdentityUserRole<int>>();
            RoleClaims = new HashSet<IdentityRoleClaim<int>>();
        }

        public virtual ICollection<IdentityUserRole<int>> UserRoles { get; set; }
        public virtual ICollection<IdentityRoleClaim<int>> RoleClaims { get; set; }

    }
}
