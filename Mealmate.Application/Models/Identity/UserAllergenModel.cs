using System;
using Mealmate.Application.Models.Base;

namespace Mealmate.Application.Models
{
    public class UserAllergenModel : BaseModel
    {
        public DateTimeOffset Created { get; set; }
        public int AllergenId { get; set; }
        public int UserId { get; set; }
    }
}