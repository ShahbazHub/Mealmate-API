using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;

namespace Mealmate.Application.Models
{
    public class MenuItemModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset Created { get; set; }

        public int MenuId { get; set; }
        public MenuModel Menu { get; set; }

        public int CuisineTypeId { get; set; }
        public CuisineTypeModel CuisineType { get; set; }

        public ICollection<MenuItemOptionModel> MenuItemOptions { get; set; }
        public ICollection<MenuItemAllergenModel> MenuItemAllergens { get; set; }
        public ICollection<MenuItemDietaryModel> MenuItemDietaries { get; set; }
    }
}