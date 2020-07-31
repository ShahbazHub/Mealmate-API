using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class OrderCreateModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int TableId { get; set; }

        [Required]
        public string OrderNumber { get; set; }

        [Required]
        public int OrderStateId { get; set; }
        public List<OrderItemCreateModel> OrderItems { get; set; }

        public OrderCreateModel()
        {
            OrderItems = new List<OrderItemCreateModel>();
        }
    }
}