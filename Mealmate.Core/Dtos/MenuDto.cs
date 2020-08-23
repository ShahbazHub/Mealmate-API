using System;
using System.Collections.Generic;
using System.Text;

namespace Mealmate.Core.Dtos
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan ServiceTimeFrom { get; set; }
        public TimeSpan ServiceTimeTo { get; set; }
        public List<MenuItemDto> MenuItems { get; set; }
        public MenuDto()
        {
            MenuItems = new List<MenuItemDto>();
        }
    }


}
