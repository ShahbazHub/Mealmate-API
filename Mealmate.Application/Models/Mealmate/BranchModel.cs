using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;

namespace Mealmate.Application.Models
{
    public class BranchModel : BaseModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTimeOffset Created { get; set; }
        public int RestaurantId { get; set; }
        public ICollection<LocationModel> Locations { get; set; }
        public ICollection<MenuModel> Menus { get; set; }
    }
}