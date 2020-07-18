using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class MenuItemOptionModel : BaseModel
    {

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
        public DateTimeOffset Created { get; set; }

        [Required]
        public int MenuItemId { get; set; }
        public MenuItemModel MenuItem { get; set; }

        [Required]
        public int OptionItemId { get; set; }
        public OptionItemModel OptionItem { get; set; }
    }
}