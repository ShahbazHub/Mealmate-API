using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Mealmate.Application.Models
{
    public class LocationModel : BaseModel
    {
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public int BranchId { get; set; }
        //public BranchModel Branch { get; set; }
        [JsonIgnore]
        public ICollection<TableModel> Tables { get; set; }

    }
}