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
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }
        public virtual ICollection<UserRestaurant> UserRestaurants { get; set; }
        public virtual ICollection<UserAllergen> UserAllergens { get; set; }
        public virtual ICollection<UserDietary> UserDietaries { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<UserOtp> UserOtps { get; set; }
        public virtual ICollection<RestroomRequest> RestroomRequests { get; set; }
        public virtual ICollection<ContactRequest> ContactRequests { get; set; }
        public virtual ICollection<BillRequest> BillRequests { get; set; }
        public virtual ICollection<UserBranch> UserBranches { get; set; }


        public User()
        {
            UserRoles = new HashSet<UserRole>();
            UserClaims = new HashSet<UserClaim>();
            UserLogins = new HashSet<UserLogin>();
            UserTokens = new HashSet<UserToken>();

            UserRestaurants = new HashSet<UserRestaurant>();
            UserAllergens = new HashSet<UserAllergen>();
            UserDietaries = new HashSet<UserDietary>();
            Orders = new HashSet<Order>();
            UserOtps = new HashSet<UserOtp>();
            UserBranches = new HashSet<UserBranch>();

            RestroomRequests = new HashSet<RestroomRequest>();
            ContactRequests = new HashSet<ContactRequest>();
            BillRequests = new HashSet<BillRequest>();
        }
    }
}
