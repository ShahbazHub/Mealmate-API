using System;
using System.Collections.Generic;
using Mealmate.DataAccess.Entities.Mealmate;

namespace Mealmate.DataAccess.Entities.Lookup
{
    public class OptionItem
    {
        public int OptionItemId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public virtual ICollection<MenuItemOption> MenuItemOptions { get; set; }

        public OptionItem()
        {
            MenuItemOptions = new HashSet<MenuItemOption>();
        }
    }
}