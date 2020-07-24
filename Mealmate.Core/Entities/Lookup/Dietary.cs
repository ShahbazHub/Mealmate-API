using Mealmate.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mealmate.Core.Entities.Lookup
{
    public class Dietary : Entity
    {
        public string Name { get; set; }
        public byte[] Photo { get; set; }

        public DateTimeOffset Created { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<OptionItemDietary> OptionItemDietaries { get; set; }

        public Dietary()
        {
            OptionItemDietaries = new HashSet<OptionItemDietary>();
        }

    }
}
