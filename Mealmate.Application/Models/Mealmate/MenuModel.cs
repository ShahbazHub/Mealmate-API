using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;

namespace Mealmate.Application.Models
{
    public class MenuModel : BaseModel
    {
        public string Name { get; set; }
        public TimeSpan ServiceTime { get; set; }
        public DateTimeOffset Created { get; set; }
        public int BranchId { get; set; }

        //public BranchModel Branch { get; set; }

        public ICollection<MenuItemModel> MenuItems { get; set; }
    }
}