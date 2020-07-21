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
        public bool IsActive { get; set; } = true;
        public int RestaurantId { get; set; }
        [JsonIgnore]
        public ICollection<LocationModel> Locations { get; set; }
        [JsonIgnore]
        public ICollection<MenuModel> Menus { get; set; }
    }
}