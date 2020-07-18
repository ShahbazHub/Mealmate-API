using System;
using System.Collections.Generic;
using Mealmate.Core.Entities.Base;
using Mealmate.Core.Entities.Lookup;

namespace Mealmate.Core.Entities
{
    public class OptionItemDietary : Entity
    {
        public int OptionItemId { get; set; }
        public virtual OptionItem OptionItem { get; set; }
        public int DietaryId { get; set; }
        public virtual Dietary Dietary { get; set; }
        public DateTimeOffset Created { get; set; }


        public OptionItemDietary()
        {
        }
    }
}