using Mealmate.Application.Models.Base;

using System;

namespace Mealmate.Application.Models
{
    public class DietaryModel : BaseModel
    {
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}