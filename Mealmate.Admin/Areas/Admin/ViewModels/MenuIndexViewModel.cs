using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.ViewModels
{
    public class MenuIndexViewModel
    {
        public int LocationId { get; set; }
        public IEnumerable<SelectListItem> Locations { get; set; }

        public List<MenuTypeAssignListViewModel> MenuTypes { get; set; }

        public MenuIndexViewModel()
        {
            MenuTypes = new List<MenuTypeAssignListViewModel>();
        }
    }
}
