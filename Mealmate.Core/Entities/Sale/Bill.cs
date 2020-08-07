using Mealmate.Core.Entities.Base;
using Mealmate.Core.Entities.Lookup;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mealmate.Core.Entities
{
    public class Bill : Entity
    {
        public int BillRequestId { get; set; }
        public BillRequest BillRequest { get; set; }
        
        public string InvoiceNumber { get; set; }
        public DateTimeOffset Created { get; set; }

        public int BillStateId { get; set; }
        public virtual BillState BillState{ get; set; }

        public virtual ICollection<BillDetail> BillDetails{ get; set; }

        public Bill()
        {
            BillDetails = new HashSet<BillDetail>();
        }
    }
}
