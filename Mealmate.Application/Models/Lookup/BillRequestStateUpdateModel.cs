using Mealmate.Application.Models.Base;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class BillRequestStateUpdateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}