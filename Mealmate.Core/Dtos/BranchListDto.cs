using System;
using System.Collections.Generic;
using System.Text;

namespace Mealmate.Core.Dtos
{
    public class BranchListDto
    {
        public int CuisineTypeId { get; set; }
        public IEnumerable<int> Allergens{ get; set; }
        public IEnumerable<int> Dietaries{ get; set; }
        public string Branch { get; set; }
        public int BranchId { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
