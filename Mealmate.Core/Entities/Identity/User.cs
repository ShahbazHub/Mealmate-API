using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Mealmate.Core.Entities
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }

        public User()
        {
            UserRoles = new HashSet<UserRole>();
            UserClaims = new HashSet<UserClaim>();
            UserLogins = new HashSet<UserLogin>();
            UserTokens = new HashSet<UserToken>();
            Restaurants = new HashSet<Restaurant>();
        }
    }
}
