using Mealmate.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mealmate.Core.Entities
{
    public class RestroomRequestState : Entity
    {
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<RestroomRequest> RestroomRequests { get; set; }

        public RestroomRequestState()
        {
            RestroomRequests = new HashSet<RestroomRequest>();
        }

    }
}
