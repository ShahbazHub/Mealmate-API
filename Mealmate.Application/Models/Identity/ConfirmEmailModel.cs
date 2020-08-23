using System;
using System.ComponentModel.DataAnnotations;

using Mealmate.Application.Models.Base;

namespace Mealmate.Application.Models
{
    public class ConfirmEmailModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Token { get; set; }
    }
}