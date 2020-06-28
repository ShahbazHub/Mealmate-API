using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.ViewModels
{
    public class QRCodeListViewModel
    {
        public int QRCodeId { get; set; }
        public DateTime Generated { get; set; }
        public string Hall { get; set; }
        public string Table { get; set; }
        public string Branch { get; set; }

        public int QRCodeTypeId { get; set; }
        public string QRCodeType { get; set; }

        public int QRCodeStateId { get; set; }
        public string QRCodeState { get; set; }

        public byte[] QRCode { get; set; }
    }
}
