using System;
using System.Collections.Generic;
using System.Text;

namespace Mealmate.Application.Models
{
    public class MenuListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan ServiceTime { get; set; }
        public List<MenuItemListModel> MenuItems { get; set; }
        public MenuListModel()
        {
            MenuItems = new List<MenuItemListModel>();
        }
    }


}
