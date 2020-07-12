using Mealmate.Application.Models.Base;
using System;
using System.Collections.Generic;

namespace Mealmate.Application.Models
{
    public class RestaurantModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }

        public int OwnerId { get; set; }
        public ICollection<BranchModel> Branches { get; set; }
    }
}
