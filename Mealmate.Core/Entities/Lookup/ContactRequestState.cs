using Mealmate.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mealmate.Core.Entities
{
    public class ContactRequestState : Entity
    {
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<ContactRequest> ContactRequests { get; set; }

        public ContactRequestState()
        {
            ContactRequests = new HashSet<ContactRequest>();
        }

    }
}
