using System;
using System.Collections.Generic;

namespace Mealmate.DataAccess.Entities.Mealmate
{
    public class Table
    {
        public int TableId { get; set; }
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<QRCode> QRCodes { get; set; }

        public Table()
        {
            QRCodes = new HashSet<QRCode>();
        }
    }
}