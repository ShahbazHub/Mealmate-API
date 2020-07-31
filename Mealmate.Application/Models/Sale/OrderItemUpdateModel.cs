using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class OrderItemUpdateModel
    {

        // For usage in order bulk update
        public int OrderItemId { get; set; }

        [Required]
        public int MenuItemId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public List<OrderItemDetailUpdateModel> OrderItemDetails { get; set; }
        public OrderItemUpdateModel()
        {
            OrderItemDetails = new List<OrderItemDetailUpdateModel>();
        }
    }
}