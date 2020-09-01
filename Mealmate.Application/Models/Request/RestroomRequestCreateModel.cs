using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class RestroomRequestCreateModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int TableId { get; set; }
        
        [Required]
        public int Gender { get; set; }
        
        [Required]
        public bool IsDisabled { get; set; }
        

    }
}