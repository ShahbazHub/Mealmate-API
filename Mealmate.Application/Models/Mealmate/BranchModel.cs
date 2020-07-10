using Mealmate.Application.Models.Base;

using System;

namespace Mealmate.Application.Models
{
    public class BranchModel : BaseModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTimeOffset Created { get; set; }
        public int RestaurantId { get; set; }
        public LocationModel Location { get; set; }
        public MenuModel Menu { get; set; }
    }
}