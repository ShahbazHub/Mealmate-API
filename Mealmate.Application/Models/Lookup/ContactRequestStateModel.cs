using Mealmate.Application.Models.Base;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class ContactRequestStateModel : BaseModel
    {
        public string Name { get; set; }
        public DateTimeOffset Created { get; set; }
        public bool IsActive { get; set; }
    }
}