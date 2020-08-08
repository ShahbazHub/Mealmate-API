using System;
using System.Collections.Generic;
using System.Text;

namespace Mealmate.Application.Models
{
    public class BranchSearchModel
    {
        public List<int> Allergens { get; set; }
        public List<int> Dietaries { get; set; }
        public List<int> CuisineTypes { get; set; }
        public List<int> Distances { get; set; }

        public int Order { get; set; }

        public BranchSearchModel()
        {
            Allergens = new List<int>();
            Dietaries = new List<int>();
            CuisineTypes = new List<int>();
            Distances = new List<int>();
        }
    }
}
