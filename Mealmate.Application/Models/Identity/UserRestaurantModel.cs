using System;
using System.ComponentModel.DataAnnotations;
using Mealmate.Application.Models.Base;

namespace Mealmate.Application.Models
{
    public class UserRestaurantModel : BaseModel
    {
        public int RestaurantId { get; set; }
        public RestaurantModel Restaurant { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }

        public DateTimeOffset Created { get; set; }
        public bool IsActive { get; set; }
    }
}