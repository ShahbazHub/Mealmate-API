using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;

namespace Mealmate.Application.Models
{
    public class ContactRequestModel : BaseModel
    {
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public DateTimeOffset RequestTime { get; set; }
        public DateTimeOffset? ResponseTime { get; set; }
        public string Remarks { get; set; }

        public int ContactRequestStateId { get; set; }
        public ContactRequestStateModel ContactRequestState { get; set; }

        public ContactRequestModel()
        {
        }

    }
}