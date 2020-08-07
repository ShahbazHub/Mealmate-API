using System;
using Mealmate.Core.Entities.Base;
using System.Collections.Generic;

namespace Mealmate.Core.Entities
{
    public class Table : Entity
    {
        //public int TableId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }

        public bool IsActive { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<QRCode> QRCodes { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<RestroomRequest> RestroomRequests { get; set; }
        public virtual ICollection<ContactRequest> ContactRequests { get; set; }
        public virtual ICollection<BillRequest> BillRequests { get; set; }


        public Table()
        {
            QRCodes = new HashSet<QRCode>();
            Orders = new HashSet<Order>();
            RestroomRequests = new HashSet<RestroomRequest>();
            ContactRequests = new HashSet<ContactRequest>();
            BillRequests = new HashSet<BillRequest>();
        }
    }
}