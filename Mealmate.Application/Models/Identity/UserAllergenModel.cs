using System;
using System.ComponentModel.DataAnnotations;
using Mealmate.Application.Models.Base;

namespace Mealmate.Application.Models
{
    public class UserAllergenModel : BaseModel
    {
        [Required]
        public DateTimeOffset Created { get; set; }

        [Required]
        public int AllergenId { get; set; }
        public AllergenModel Allergen { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}