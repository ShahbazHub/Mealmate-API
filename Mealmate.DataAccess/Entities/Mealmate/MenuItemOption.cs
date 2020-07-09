using System;
using Mealmate.DataAccess.Entities.Lookup;
using Mealmate.DataAccess.Entities.Mealmate;

namespace Mealmate.DataAccess.Entities.Mealmate
{
    public class MenuItemOption
    {
        public int MenuItemOptionId { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset Created { get; set; }

        public int MenuItemId { get; set; }
        public virtual MenuItem MenuItem { get; set; }

        public int OptionItemId { get; set; }
        public virtual OptionItem OptionItem { get; set; }
    }
}