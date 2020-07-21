using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mealmate.Application.Models
{
    public class LocationUpdateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; }

    }
}