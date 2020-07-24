using System;
using System.Collections.Generic;
using Mealmate.Core.Entities.Base;
using Mealmate.Core.Entities.Lookup;

namespace Mealmate.Core.Entities
{
    public class MenuItemDietary : Entity
    {
        public DateTimeOffset Created { get; set; }

        public int DietaryId { get; set; }
        public virtual Dietary Dietary { get; set; }

        public int MenuItemId { get; set; }
        public virtual MenuItem MenuItem { get; set; }
        public bool IsActive { get; set; }

        public MenuItemDietary()
        {
        }
    }
}