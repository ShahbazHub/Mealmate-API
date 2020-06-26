using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.ViewModels
{
    public class IngredientAssignListViewModel
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
