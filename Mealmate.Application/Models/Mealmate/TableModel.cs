using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;

namespace Mealmate.Application.Models
{
    public class TableModel : BaseModel
    {
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }

        public bool IsActive { get; set; } = true;
        public int LocationId { get; set; }
        //public LocationModel Location { get; set; }

        public ICollection<QRCodeModel> QRCodes { get; set; }
    }
}