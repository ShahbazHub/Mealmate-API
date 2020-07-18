using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class OptionItemAllergenModel : BaseModel
    {
        [Required]
        public int AllergenId { get; set; }

        [Required]
        public int OptionItemId { get; set; }

        [Required]
        public DateTimeOffset Created { get; set; }

        public OptionItemModel OptionItem { get; set; }
        public AllergenModel Allergen { get; set; }
    }
}