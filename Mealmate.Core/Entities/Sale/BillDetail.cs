using Mealmate.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mealmate.Core.Entities
{
    public class BillDetail : Entity
    {
        public int BillId { get; set; }
        public virtual Bill Bill { get; set; }

        public DateTimeOffset Created { get; set; }

        public int OrderId { get; set; }
    }
}
