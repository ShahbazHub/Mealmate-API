using System;
using System.ComponentModel.DataAnnotations;
using Mealmate.Application.Models.Base;

namespace Mealmate.Application.Models
{
    public class UserAllergenCreateModel
    {
        [Required]
        public int AllergenId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}