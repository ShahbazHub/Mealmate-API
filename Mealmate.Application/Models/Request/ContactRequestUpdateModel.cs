using Mealmate.Application.Models.Base;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mealmate.Application.Models
{
    public class ContactRequestUpdateModel
    {
        public string Remarks { get; set; }

        [Required]
        public int ContactRequestStateId { get; set; }

        public ContactRequestUpdateModel()
        {
        }

    }
}