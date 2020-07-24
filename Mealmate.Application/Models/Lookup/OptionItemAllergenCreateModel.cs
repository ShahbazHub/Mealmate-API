using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class OptionItemAllergenCreateModel
    {
        [Required]
        public int AllergenId { get; set; }

        [Required]
        public int OptionItemId { get; set; }

        [Required]
        public bool IsActive { get; set; }

    }
}