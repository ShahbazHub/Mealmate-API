using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.ViewModels
{
    public class MenuIndexViewModel
    {
        public List<MenuTypeAssignListViewModel> MenuTypes { get; set; }
        public List<IngredientAssignListViewModel> Ingredients { get; set; }

        public MenuIndexViewModel()
        {
            Ingredients = new List<IngredientAssignListViewModel>();
            MenuTypes = new List<MenuTypeAssignListViewModel>();
        }
    }
}
