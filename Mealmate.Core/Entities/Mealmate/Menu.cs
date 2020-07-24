using System;
using System.Collections.Generic;
using Mealmate.Core.Entities.Base;

namespace Mealmate.Core.Entities
{
    public class Menu : Entity
    {
        //public int MenuId { get; set; }
        public string Name { get; set; }
        public TimeSpan ServiceTime { get; set; }
        public DateTimeOffset Created { get; set; }
        public bool IsActive { get; set; }
        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }

        public virtual ICollection<MenuItem> MenuItems { get; set; }
        public Menu()
        {
            MenuItems = new HashSet<MenuItem>();
        }
    }
}