using System;
using System.Collections.Generic;
using Mealmate.Core.Entities.Base;

namespace Mealmate.Core.Entities
{
    public class OrderItem : Entity
    {
        public int MenuItemId { get; set; }
        public virtual MenuItem MenuItem { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public DateTimeOffset Created { get; set; }

        public ICollection<OrderItemDetail> OrderItemDetails { get; set; }
        public OrderItem()
        {
            OrderItemDetails = new HashSet<OrderItemDetail>();
        }
    }
}