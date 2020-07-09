using System;

namespace Mealmate.DataAccess.Entities.Mealmate
{
    public class QRCode
    {
        public int QRCodeId { get; set; }
        public byte[] Code { get; set; }
        public DateTimeOffset Created { get; set; }

        public int TableId { get; set; }
        public virtual Table Table { get; set; }
    }
}