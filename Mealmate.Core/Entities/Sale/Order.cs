using System;
using System.Collections.Generic;
using Mealmate.Core.Entities.Base;
using Mealmate.Core.Entities.Lookup;

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

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public int OrderStateId { get; set; }
        public virtual OrderState OrderState { get; set; }


        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

    }
}