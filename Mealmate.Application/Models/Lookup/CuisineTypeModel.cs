using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class CuisineTypeModel : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTimeOffset Created { get; set; }

        public ICollection<MenuItemModel> MenuItems { get; set; }
    }
}