using System;
using System.Collections.Generic;
using Mealmate.Core.Entities.Base;

namespace Mealmate.Core.Entities
{
    public class Order : Entity
    {
        public int CustomerId { get; set; }
        public virtual User Customer { get; set; }

        public int TableId { get; set; }
        public virtual Table Table { get; set; }

        public string OrderNumber { get; set; }
        public DateTimeOffset OrderDate { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

    }
}