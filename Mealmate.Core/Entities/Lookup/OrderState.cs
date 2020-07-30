using Mealmate.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mealmate.Core.Entities.Lookup
{
    public class OrderState : Entity
    {
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public OrderState()
        {
            Orders = new HashSet<Order>();
        }

    }
}
