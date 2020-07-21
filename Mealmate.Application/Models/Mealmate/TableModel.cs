using Mealmate.Application.Models.Base;

using System;

namespace Mealmate.Application.Models
{
    public class TableModel : BaseModel
    {
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }

        public bool IsActive { get; set; }
        public int LocationId { get; set; }
        //public LocationModel Location { get; set; }

        //public QRCodeModel QRCode { get; set; }
    }
}