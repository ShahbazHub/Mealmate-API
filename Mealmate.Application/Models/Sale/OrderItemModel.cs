using Mealmate.Application.Models.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Mealmate.Application.Models
{
    public class OrderItemModel : BaseModel
    {
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; }

        public string MenuItemDescription { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public DateTimeOffset Created { get; set; }

        public ICollection<OrderItemDetailModel> OrderItemDetails { get; set; }
        public OrderItemModel()
        {
            OrderItemDetails = new HashSet<OrderItemDetailModel>();
        }
    }
}