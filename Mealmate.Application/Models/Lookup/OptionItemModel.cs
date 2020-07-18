using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class OptionItemModel : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTimeOffset Created { get; set; }
        public ICollection<MenuItemOptionModel> MenuItemOptions { get; set; }

        [Required]
        public int BranchId { get; set; }
        //public BranchModel Branch { get; set; }
    }
}