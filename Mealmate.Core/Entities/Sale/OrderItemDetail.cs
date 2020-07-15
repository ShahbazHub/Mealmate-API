using System;
using Mealmate.Core.Entities.Base;

namespace Mealmate.Core.Entities
{
    public class OrderItemDetail : Entity
    {
        public int OrderItemId { get; set; }
        public virtual OrderItem OrderItem { get; set; }

        public int MenuItemOptionId { get; set; }
        public virtual MenuItemOption MenuItemOption { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public DateTimeOffset Created { get; set; }

    }
}