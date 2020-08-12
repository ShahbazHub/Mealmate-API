using Mealmate.Application.Models.Base;

using System;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class AllergenListModel : BaseModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}