using System;
using Mealmate.Core.Entities.Base;
using System.Collections.Generic;

namespace Mealmate.Core.Entities
{
    public class Restaurant : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }
        public bool IsActive { get; set; } = true;
        public byte[] Photo { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }
        public virtual ICollection<UserRestaurant> UserRestaurants { get; set; }


        public Restaurant()
        {
            Branches = new HashSet<Branch>();
            UserRestaurants = new HashSet<UserRestaurant>();
        }
    }
}
