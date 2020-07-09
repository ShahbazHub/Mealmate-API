using Mealmate.Application.Models.Base;
using System;

namespace Mealmate.Application.Models
{
    public class RestaurantModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
