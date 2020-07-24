using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class MenuItemDietaryCreateModel
    {
        [Required]
        public int DietaryId { get; set; }

        [Required]
        public int MenuItemId { get; set; }

        [Required]
        public bool IsActive { get; set; }

    }
}