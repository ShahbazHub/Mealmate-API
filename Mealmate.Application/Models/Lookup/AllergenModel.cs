using Mealmate.Application.Models.Base;

using System;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class AllergenModel : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public byte[] Photo { get; set; }

        public DateTimeOffset Created { get; set; }
    }
}