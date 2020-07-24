using System;
using System.ComponentModel.DataAnnotations;
using Mealmate.Application.Models.Base;

namespace Mealmate.Application.Models
{
    public class UserDietaryUpdateModel
    {
        [Required]
        public bool IsActive { get; set; }

    }
}