using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class BillRequestUpdateModel
    {
        public string Remarks { get; set; }

        [Required]
        public int BillRequestStateId { get; set; }

        public BillRequestUpdateModel()
        {
        }

    }
}