using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class OptionItemDetailCreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int BranchId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public List<OptionItemDetailCreateAllergenModel> Allergens { get; set; }
        public List<OptionItemDetailCreateDietaryModel> Dietaries { get; set; }

        public OptionItemDetailCreateModel()
        {
            Allergens = new List<OptionItemDetailCreateAllergenModel>();
            Dietaries = new List<OptionItemDetailCreateDietaryModel>();
        }

    }

    public class OptionItemDetailCreateAllergenModel
    {
        public int OptionItemAllergenId { get; set; } = 0;

        [Required]
        public int AllergenId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }

    public class OptionItemDetailCreateDietaryModel
    {
        public int OptionItemDietaryId { get; set; } = 0;

        [Required]
        public int DietaryId { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}