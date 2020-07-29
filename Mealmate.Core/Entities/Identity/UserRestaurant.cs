using System;

using Mealmate.Core.Entities.Base;

using Microsoft.AspNetCore.Identity;

namespace Mealmate.Core.Entities
{
    public class UserRestaurant : Entity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int RestaurantId { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public DateTimeOffset Created { get; set; }

        public bool IsActive { get; set; } = true;
        public bool isOwner { get; set; }
    }
}
