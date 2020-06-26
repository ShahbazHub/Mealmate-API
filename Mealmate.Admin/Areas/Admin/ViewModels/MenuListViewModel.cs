using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.ViewModels
{
    public class MenuListViewModel
    {
        public int MenuId { get; set; }
        public string Title { get; set; }
        public int MenuTypeId { get; set; }
        public string MenuType { get; set; }
        public int MenuStateId { get; set; }
        public string MenuState { get; set; }
    }
}
