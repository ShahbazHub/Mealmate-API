using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class MenuItemAllergenUpdateModel
    {
        [Required]
        public bool IsActive { get; set; }

    }
}