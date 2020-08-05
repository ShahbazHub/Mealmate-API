using Mealmate.Application.Models.Base;

using System;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class DietaryCreateModel
    {
        [Required]
        public string Name { get; set; }

        public byte[] Photo { get; set; }
        public byte[] PhotoSelected { get; set; }

        [Required]
        public bool IsActive { get; set; }

    }
}