using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mealmate.Application.Models
{
    public class BranchInfoModel
    {
        public string Address { get; set; }
        public string Description { get; set; }
        public string ContactNumber { get; set; }
        public TimeSpan ServiceTimeFrom { get; set; }
        public TimeSpan ServiceTimeTo { get; set; }

    }
}
