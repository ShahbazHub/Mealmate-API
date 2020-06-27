using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.ViewModels
{
    public class QRCodeIndexViewModel
    {
        public List<QRCodeTypeAssignListViewModel> QRCodeTypes { get; set; }

        public int HallId { get; set; }
        public IEnumerable<SelectListItem> Halls { get; set; }

        public List<TableAssignListViewModel> Tables { get; set; }

    }
}
