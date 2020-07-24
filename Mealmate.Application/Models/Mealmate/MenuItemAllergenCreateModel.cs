using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class MenuItemAllergenCreateModel
    {
        [Required]
        public int AllergenId { get; set; }

        [Required]
        public int MenuItemId { get; set; }

        [Required]
        public bool IsActive { get; set; }

    }
}