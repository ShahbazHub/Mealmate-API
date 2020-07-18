using System;
using System.Collections.Generic;
using Mealmate.Core.Entities.Base;
using Mealmate.Core.Entities.Lookup;

namespace Mealmate.Core.Entities
{
    public class OptionItemAllergen : Entity
    {
        public int OptionItemId { get; set; }
        public virtual OptionItem OptionItem { get; set; }
        public int AllergenId { get; set; }
        public virtual Allergen Allergen { get; set; }
        public DateTimeOffset Created { get; set; }


        public OptionItemAllergen()
        {
        }
    }
}