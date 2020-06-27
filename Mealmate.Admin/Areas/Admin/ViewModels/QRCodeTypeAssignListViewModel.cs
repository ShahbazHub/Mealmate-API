using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.ViewModels
{
    public class QRCodeTypeAssignListViewModel
    {
        public int QRCodeTypeId { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
