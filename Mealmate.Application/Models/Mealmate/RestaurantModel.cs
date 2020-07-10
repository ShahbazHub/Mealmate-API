using Mealmate.Application.Models.Base;
using System;

namespace Mealmate.Application.Models
{
    public class RestaurantModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }

        public int OwnerId { get; set; }
        public BranchModel Branch { get; set; }
    }
}
