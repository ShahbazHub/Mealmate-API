using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class MenuCreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public TimeSpan ServiceTimeFrom { get; set; }
        public TimeSpan ServiceTimeTo { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

    }
}