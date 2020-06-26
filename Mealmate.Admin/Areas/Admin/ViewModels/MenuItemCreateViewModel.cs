using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.ViewModels
{
    public class MenuItemCreateViewModel
    {
        public int IngredientId { get; set; }
        public IEnumerable<SelectListItem> Ingredients { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
