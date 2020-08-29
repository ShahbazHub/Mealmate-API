using Mealmate.Application.Models.Base;
using Mealmate.Core.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Mealmate.Application.Models
{
    public class OrderModel : BaseModel
    {
        public int CustomerId { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public User Customer { get; set; }
        
        public int TableId { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public TableModel Table { get; set; }

        public int OrderStateId { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
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