using Mealmate.Application.Models.Base;
using System;
using System.Collections.Generic;

namespace Mealmate.Application.Models
{
    public class OptionItemModel : BaseModel
    {
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public int BranchId { get; set; }
        public ICollection<MenuItemOptionModel> MenuItemOptions { get; set; }

        //public BranchModel Branch { get; set; }
    }
}