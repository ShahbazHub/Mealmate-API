using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class OrderItemDetailUpdateModel
    {

        // to be used for bulk updation
        // order
        // order item
        public int OrderItemDetailId { get; set; }

        [Required]
        public int MenuItemOptionId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

    }
}