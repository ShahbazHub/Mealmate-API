using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.ViewModels
{
    public class TableAssignListViewModel
    {
        public int TableId { get; set; }
        public string Number { get; set; }
        public bool IsSelected { get; set; }
    }
}
