using System;
using System.Collections.Generic;
using Mealmate.Core.Entities.Base;
using Mealmate.Core.Entities.Lookup;

namespace Mealmate.Core.Entities
{
    public class MenuItemAllergen : Entity
    {
        public DateTimeOffset Created { get; set; }

        public int AllergenId { get; set; }
        public virtual Allergen Allergen { get; set; }

        public int MenuItemId { get; set; }
        public virtual MenuItem MenuItem { get; set; }

        public MenuItemAllergen()
        {
        }
    }
}