using System;
using System.Collections.Generic;
using Mealmate.Core.Entities.Base;

namespace Mealmate.Core.Entities
{
    public class MenuItemOption : Entity
    {
        //public int MenuItemOptionId { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset Created { get; set; }

        public int MenuItemId { get; set; }
        public virtual MenuItem MenuItem { get; set; }

        public int OptionItemId { get; set; }
        public virtual OptionItem OptionItem { get; set; }

        public virtual ICollection<OrderItemDetail> OrderItemDetails { get; set; }
        public MenuItemOption()
        {
            OrderItemDetails = new HashSet<OrderItemDetail>();
        }
    }
}