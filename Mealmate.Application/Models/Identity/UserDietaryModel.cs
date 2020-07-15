using System;
using Mealmate.Application.Models.Base;

namespace Mealmate.Application.Models
{
    public class UserDietaryModel : BaseModel
    {
        public DateTimeOffset Created { get; set; }
        public int DietaryId { get; set; }
        public int UserId { get; set; }
    }
}