using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class RestroomRequestUpdateModel
    {

        [Required]
        public int RestroomRequestStateId { get; set; }
        public string Remarks { get; set; }
    }
}