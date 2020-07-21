using Mealmate.Application.Models.Base;

using System;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class TableCreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public int LocationId { get; set; }
    }
}