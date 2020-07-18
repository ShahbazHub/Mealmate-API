using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;

namespace Mealmate.Application.Models
{
    public class MenuItemAllergenModel : BaseModel
    {
        public DateTimeOffset Created { get; set; }

        public int AllergenId { get; set; }
        public AllergenModel Allergen { get; set; }
        public int MenuItemId { get; set; }
    }
}