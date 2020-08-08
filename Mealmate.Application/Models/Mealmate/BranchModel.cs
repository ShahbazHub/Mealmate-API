using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mealmate.Application.Models
{
    public class BranchModel : BaseModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTimeOffset Created { get; set; }
        public bool IsActive { get; set; }
        public int RestaurantId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public RestaurantModel Restaurant { get; set; }
        public ICollection<LocationModel> Locations { get; set; }
        public ICollection<MenuModel> Menus { get; set; }
        public int TotalDishes { get; set; }
        public int FilteredDishes { get; set; }

    }
}