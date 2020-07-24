using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class MenuItemCreateModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        public int MenuId { get; set; }
        [Required]
        public int CuisineTypeId { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}