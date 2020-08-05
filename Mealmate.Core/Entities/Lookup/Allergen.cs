using Mealmate.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mealmate.Core.Entities.Lookup
{
    public class Allergen : Entity
    {
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public byte[] PhotoSelected { get; set; }
        public DateTimeOffset Created { get; set; }

        public bool IsActive { get; set; }
        public virtual ICollection<OptionItemAllergen> OptionItemAllergens { get; set; }

        public Allergen()
        {
            OptionItemAllergens = new HashSet<OptionItemAllergen>();
        }

    }
}
