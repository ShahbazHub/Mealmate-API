using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;

namespace Mealmate.Application.Models
{
    public class OrderModel : BaseModel
    {
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public string OrderNumber { get; set; }
        public DateTimeOffset OrderDate { get; set; }

        public ICollection<OrderItemModel> OrderItems { get; set; }

        public OrderModel()
        {
            OrderItems = new HashSet<OrderItemModel>();
        }

    }
}