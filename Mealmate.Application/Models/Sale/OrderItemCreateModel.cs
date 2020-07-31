using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class OrderItemCreateModel
    {
        [Required]
        public int MenuItemId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int OrderId { get; set; }

        public List<OrderItemDetailCreateModel> OrderItemDetails { get; set; }
        public OrderItemCreateModel()
        {
            OrderItemDetails = new List<OrderItemDetailCreateModel>();
        }
    }
}