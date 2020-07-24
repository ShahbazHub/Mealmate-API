using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;

namespace Mealmate.Application.Models
{
    public class MenuItemDietaryModel : BaseModel
    {
        public DateTimeOffset Created { get; set; }

        public int DietaryId { get; set; }
        public DietaryModel Dietary { get; set; }
        public int MenuItemId { get; set; }
        public bool IsActive { get; set; }

    }
}