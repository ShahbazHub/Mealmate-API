using Mealmate.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mealmate.Core.Entities.Lookup
{
    public class BillState : Entity
    {
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        public BillState()
        {
            Bills = new HashSet<Bill>();
        }
    }
}
