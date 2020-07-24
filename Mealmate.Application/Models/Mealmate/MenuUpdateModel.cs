using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class MenuUpdateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public TimeSpan ServiceTime { get; set; }

        [Required]
        public bool IsActive { get; set; }

    }
}