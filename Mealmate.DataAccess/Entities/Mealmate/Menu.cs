using System;
using System.Collections.Generic;
using Mealmate.DataAccess.Entities.Mealmate;

namespace Mealmate.DataAccess.Entities.Mealmate
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public TimeSpan ServiceTime { get; set; }
        public DateTimeOffset Created { get; set; }

        public int BranchId { get; set; }
        public virtual Branch Branch { get; set; }

        public virtual ICollection<MenuItem> MenuItems { get; set; }
        public Menu()
        {
            MenuItems = new HashSet<MenuItem>();
        }
    }
}