using System;
using System.Collections.Generic;
using Mealmate.Core.Entities.Base;
using Mealmate.Core.Entities.Lookup;

namespace Mealmate.Core.Entities
{
    public class RestroomRequest : Entity
    {
        public int CustomerId { get; set; }
        public virtual User Customer { get; set; }

        public int TableId { get; set; }
        public virtual Table Table { get; set; }

        public DateTimeOffset RequestTime { get; set; }
        public DateTimeOffset? ResponseTime { get; set; }
        public string Remarks { get; set; }

        public int RestroomRequestStateId { get; set; }
        public virtual RestroomRequestState RestRoomRequestState { get; set; }

        public RestroomRequest()
        {
        }

    }
}