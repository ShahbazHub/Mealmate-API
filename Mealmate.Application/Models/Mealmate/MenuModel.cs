using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;

namespace Mealmate.Application.Models
{
    public class MenuModel : BaseModel
    {
        public string Name { get; set; }
        public TimeSpan ServiceTimeFrom { get; set; }
        public TimeSpan ServiceTimeTo { get; set; }
        public DateTimeOffset Created { get; set; }
        public int BranchId { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<MenuItemModel> MenuItems { get; set; }

    }
}