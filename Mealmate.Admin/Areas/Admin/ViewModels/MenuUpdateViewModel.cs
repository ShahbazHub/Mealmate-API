using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.ViewModels
{
    public class MenuUpdateViewModel
    {
        public int MenuId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MenuTypeId { get; set; }
        public IEnumerable<SelectListItem> MenuTypes { get; set; }

        public int HallId { get; set; }
        public IEnumerable<SelectListItem> Halls { get; set; }

        public List<MenuItemCreateViewModel> MenuItems { get; set; }

        public MenuUpdateViewModel()
        {
            MenuItems = new List<MenuItemCreateViewModel>();
        }
    }
}
