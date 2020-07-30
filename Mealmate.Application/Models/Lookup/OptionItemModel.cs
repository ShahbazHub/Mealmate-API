using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class OptionItemModel : BaseModel
    {
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public int BranchId { get; set; }
        public bool IsActive { get; set; }

        public ICollection<MenuItemOptionModel> MenuItemOptions { get; set; }
        public ICollection<OptionItemAllergenModel> OptionItemAllergens { get; set; }
        public ICollection<OptionItemDietaryModel> OptionItemDietaries{ get; set; }

    }
}