using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.ViewModels
{
    public class MenuIndexViewModel
    {
        public int BranchId { get; set; }
        public IEnumerable<SelectListItem> Branches { get; set; }

        public List<MenuTypeAssignListViewModel> MenuTypes { get; set; }
        public List<IngredientAssignListViewModel> Ingredients { get; set; }

        public MenuIndexViewModel()
        {
            Ingredients = new List<IngredientAssignListViewModel>();
            MenuTypes = new List<MenuTypeAssignListViewModel>();
        }
    }
}
