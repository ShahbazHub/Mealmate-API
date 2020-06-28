using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Restaurant.ViewModels
{
    public class BranchListViewModel
    {
        public int BranchId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int Halls { get; set; }

        public int StateId { get; set; }
        public string State { get; set; }
    }
}
