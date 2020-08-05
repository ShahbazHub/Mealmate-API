using Mealmate.Application.Models.Base;

using System;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class DietaryModel : BaseModel
    {
        public string Name { get; set; }
        public byte[] Photo { get; set; }
        public byte[] PhotoSelected { get; set; }
        public DateTimeOffset Created { get; set; }
        public bool IsActive { get; set; }

    }
}