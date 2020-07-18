using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;

namespace Mealmate.Application.Models
{
    public class MenuItemOptionModel : BaseModel
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset Created { get; set; }

        public int MenuItemId { get; set; }
        public MenuItemModel MenuItem { get; set; }

        public int OptionItemId { get; set; }
        public OptionItemModel OptionItem { get; set; }
    }
}