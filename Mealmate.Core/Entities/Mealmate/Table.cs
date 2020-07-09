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

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<QRCode> QRCodes { get; set; }

        public Table()
        {
            QRCodes = new HashSet<QRCode>();
        }
    }
}