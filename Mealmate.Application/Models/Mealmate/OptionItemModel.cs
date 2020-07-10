using Mealmate.Application.Models.Base;
using System;

namespace Mealmate.Application.Models
{
    public class OptionItemModel : BaseModel
    {
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}