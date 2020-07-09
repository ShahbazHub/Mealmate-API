using System;
using Mealmate.Core.Entities.Base;

namespace Mealmate.Core.Entities
{
    public class QRCode : Entity
    {
        //public int QRCodeId { get; set; }
        public byte[] Code { get; set; }
        public DateTimeOffset Created { get; set; }

        public int TableId { get; set; }
        public virtual Table Table { get; set; }
    }
}