using System;
using System.ComponentModel.DataAnnotations;

using Mealmate.Application.Models.Base;

namespace Mealmate.Application.Models
{
    public class SignOutModel
    {
        [Required]
        public string Email { get; set; }

        public string ClientId { get; set; }

    }
}