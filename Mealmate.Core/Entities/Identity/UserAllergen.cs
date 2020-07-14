using System;
using System.Collections.Generic;
using Mealmate.Core.Entities.Base;
using Mealmate.Core.Entities.Lookup;

namespace Mealmate.Core.Entities
{
    public class UserAllergen : Entity
    {
        public DateTimeOffset Created { get; set; }

        public int AllergenId { get; set; }
        public virtual Allergen Allergen { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public UserAllergen()
        {
        }
    }
}