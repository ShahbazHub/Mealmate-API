using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class OptionItemDietaryUpdateModel
    {
        [Required]
        public bool IsActive { get; set; }
    }
}