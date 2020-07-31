using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class OrderUpdateModel
    {
        [Required]
        public int OrderStateId { get; set; }
        public List<OrderItemUpdateModel> OrderItems { get; set; }

        public OrderUpdateModel()
        {
            OrderItems = new List<OrderItemUpdateModel>();
        }
    }
}