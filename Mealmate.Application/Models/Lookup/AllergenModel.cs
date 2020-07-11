using Mealmate.Application.Models.Base;

using System;

namespace Mealmate.Application.Models
{
    public class AllergenModel : BaseModel
    {
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}