using System;
using System.ComponentModel.DataAnnotations;
using Mealmate.Application.Models.Base;

namespace Mealmate.Application.Models
{
    public class UserAllergenUpdateModel
    {
        [Required]
        public bool IsActive { get; set; }
    }
}