using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class OptionItemAllergenModel : BaseModel
    {
        public int AllergenId { get; set; }
        public int OptionItemId { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset Created { get; set; }
        public OptionItemModel OptionItem { get; set; }
        public AllergenModel Allergen { get; set; }
    }
}