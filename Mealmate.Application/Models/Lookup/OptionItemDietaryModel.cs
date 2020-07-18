using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class OptionItemDietaryModel : BaseModel
    {
        [Required]
        public int DietaryId { get; set; }

        [Required]
        public int OptionItemId { get; set; }

        [Required]
        public DateTimeOffset Created { get; set; }

        public OptionItemModel OptionItem { get; set; }
        public DietaryModel Dietary { get; set; }
    }
}