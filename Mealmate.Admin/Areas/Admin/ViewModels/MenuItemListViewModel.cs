using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.ViewModels
{
    public class MenuItemListViewModel
    {
        public List<MenuItemCreateViewModel> MenuItems { get; set; }

        public MenuItemListViewModel()
        {
            MenuItems = new List<MenuItemCreateViewModel>();
        }
    }
}
