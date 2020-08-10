using System;
using System.Collections.Generic;
using System.Text;

namespace Mealmate.Application.Models
{
    public class BranchResultModel
    {
        public int BranchId { get; set; }
        public string Branch { get; set; }
        public string Restaurant { get; set; }
        public byte[] Photo { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        public int TotalDishes { get; set; }
        public int FilteredDishes { get; set; }
    }
}
