using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;

namespace Mealmate.Application.Models
{
    public class BillRequestModel : BaseModel
    {
        public int CustomerId { get; set; }
        public int TableId { get; set; }
        public TableModel Table { get; set; }
        public UserModel Customer { get; set; }

        public DateTimeOffset RequestTime { get; set; }
        public DateTimeOffset? ResponseTime { get; set; }
        public string Remarks { get; set; }

        public int BillRequestStateId { get; set; }
        public BillRequestStateModel BillRequestState { get; set; }

        public BillRequestModel()
        {
        }

    }
}