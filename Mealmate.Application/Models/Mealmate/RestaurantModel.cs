using Mealmate.Application.Models.Base;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mealmate.Application.Models
{
    public class RestaurantModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset Created { get; set; }

        public int OwnerId { get; set; }

        [JsonIgnore]
        public ICollection<BranchModel> Branches { get; set; }
    }
}
