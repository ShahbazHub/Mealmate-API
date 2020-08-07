using Mealmate.Application.Models.Base;
using Mealmate.Core.Entities;
using System;
using System.Collections.Generic;

namespace Mealmate.Application.Models
{
    public class OrderModel : BaseModel
    {
        public int CustomerId { get; set; }
        public User Customer { get; set; }
        
        public int TableId { get; set; }
        public TableModel Table { get; set; }

        public int OrderStateId { get; set; }
        public OrderStateModel OrderState { get; set; }

        public string OrderNumber { get; set; }
        public DateTimeOffset OrderDate { get; set; }

        public ICollection<OrderItemModel> OrderItems { get; set; }

        public OrderModel()
        {
            OrderItems = new HashSet<OrderItemModel>();
        }

    }
}