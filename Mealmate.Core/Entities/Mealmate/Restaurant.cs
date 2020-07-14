using System;
using Mealmate.Core.Entities.Base;
using System.Collections.Generic;

namespace Mealmate.Core.Entities
{
    public class Restaurant : Entity
    {
        //public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }

        public int OwnerId { get; set; }
        public virtual User Owner { get; set; }

        public int CuisineTypeId { get; set; }
        public CuisineType CuisineType { get; set; }

        public virtual ICollection<Branch> Branches { get; set; }

        public Restaurant()
        {
            Branches = new HashSet<Branch>();
        }
    }
}
