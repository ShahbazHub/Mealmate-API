using System;
using System.Collections.Generic;
using Mealmate.Core.Entities.Base;

namespace Mealmate.Core.Entities
{
    public class OptionItem : Entity
    {
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public virtual ICollection<MenuItemOption> MenuItemOptions { get; set; }

        public int BranchId { get; set; }

        public virtual Branch Branch { get; set; }

        public OptionItem()
        {
            MenuItemOptions = new HashSet<MenuItemOption>();
        }
    }
}