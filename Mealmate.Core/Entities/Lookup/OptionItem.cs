using System;
using System.Collections.Generic;
using Mealmate.Core.Entities.Base;

namespace Mealmate.Core.Entities
{
    public class OptionItem : Entity
    {
        //public int OptionItemId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public virtual ICollection<MenuItemOption> MenuItemOptions { get; set; }

        public OptionItem()
        {
            MenuItemOptions = new HashSet<MenuItemOption>();
        }
    }
}