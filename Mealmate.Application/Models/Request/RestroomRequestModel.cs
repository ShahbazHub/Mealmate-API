using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;

namespace Mealmate.Application.Models
{
    public class RestroomRequestModel : BaseModel
    {
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public DateTimeOffset RequestTime { get; set; }
        public DateTimeOffset? ResponseTime { get; set; }
        public string Remarks { get; set; }

        public int RestroomRequestStateId { get; set; }
        public RestroomRequestStateModel RestroomRequestState { get; set; }

        public RestroomRequestModel()
        {
        }

    }
}