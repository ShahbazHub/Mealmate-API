using Mealmate.Application.Models.Base;

using System;

namespace Mealmate.Application.Models
{
    public class LocationModel : BaseModel
    {
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public int BranchId { get; set; }
        //public BranchModel Branch { get; set; }

        public TableModel Table { get; set; }

    }
}