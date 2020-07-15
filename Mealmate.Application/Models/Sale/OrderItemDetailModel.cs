using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;

namespace Mealmate.Application.Models
{
    public class OrderItemDetailModel : BaseModel
    {
        public int OrderItemId { get; set; }
        public int MenuItemOptionId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset Created { get; set; }

    }
}