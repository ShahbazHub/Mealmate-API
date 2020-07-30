using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class OptionItemDetailUpdateModel
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public List<OptionItemDetailUpdateAllergenModel> Allergens { get; set; }
        public List<OptionItemDetailUpdateDietaryModel> Dietaries { get; set; }

    }

    public class OptionItemDetailUpdateAllergenModel
    {
        public int OptionItemAllergenId { get; set; }

        [Required]
        public int AllergenId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }

    public class OptionItemDetailUpdateDietaryModel
    {
        public int OptionItemDietaryId { get; set; }

        [Required]
        public int DietaryId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}