using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.ViewModels
{
    public class MenuCreateViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int MenuTypeId { get; set; }
        public IEnumerable<SelectListItem> MenuTypes { get; set; }

        public int LocationId { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }

        public List<AllergenAssignListViewModel> Allergens { get; set; }
        public List<DietaryAssignListViewModel> Dietaries { get; set; }

        public List<MenuItemCreateViewModel> MenuItems { get; set; }

        public MenuCreateViewModel()
        {
            MenuItems = new List<MenuItemCreateViewModel>();
        }
    }
}
