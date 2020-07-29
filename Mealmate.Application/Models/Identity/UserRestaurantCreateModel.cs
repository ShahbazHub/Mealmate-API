using System;
using System.ComponentModel.DataAnnotations;
using Mealmate.Application.Models.Base;

namespace Mealmate.Application.Models
{
    public class UserRestaurantCreateModel
    {
        [Required]
        public int RestaurantId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;
        public bool IsOwner { get; set; } 
    }
}