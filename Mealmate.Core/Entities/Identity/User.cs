using System;
using System.Collections.Generic;
using Mealmate.Core.Entities.Lookup;
using Microsoft.AspNetCore.Identity;

namespace Mealmate.Core.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset Created { get; set; }

        public virtual ICollection<IdentityUserRole<int>> UserRoles { get; set; }
        public virtual ICollection<IdentityUserLogin<int>> UserLogins { get; set; }
        public virtual ICollection<IdentityUserClaim<int>> UserClaims { get; set; }
        public virtual ICollection<IdentityUserToken<int>> UserTokens { get; set; }
        public virtual ICollection<Restaurant> Restaurants { get; set; }
        public virtual ICollection<UserAllergen> UserAllergens { get; set; }
        public virtual ICollection<UserDietary> UserDietaries { get; set; }

        public User()
        {
            UserRoles = new HashSet<IdentityUserRole<int>>();
            UserClaims = new HashSet<IdentityUserClaim<int>>();
            UserLogins = new HashSet<IdentityUserLogin<int>>();
            UserTokens = new HashSet<IdentityUserToken<int>>();
            Restaurants = new HashSet<Restaurant>();
            UserAllergens = new HashSet<UserAllergen>();
            UserDietaries = new HashSet<UserDietary>();
        }
    }
}
