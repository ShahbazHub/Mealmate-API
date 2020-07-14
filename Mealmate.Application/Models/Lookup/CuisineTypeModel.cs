using Mealmate.Application.Models.Base;

using System;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class CuisineTypeModel : BaseModel
    {
        [Required]
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}