using System;
using System.Collections.Generic;
using System.Text;

namespace Mealmate.Application.Models
{
    public class MenuItemSearchModel
    {
        public List<int> Allergens { get; set; }
        public List<int> Dietaries { get; set; }

        public MenuItemSearchModel()
        {
            Allergens = new List<int>();
            Dietaries = new List<int>();
        }
    }
}
