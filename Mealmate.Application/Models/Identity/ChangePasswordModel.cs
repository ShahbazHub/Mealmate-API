using System;
using System.ComponentModel.DataAnnotations;

using Mealmate.Application.Models.Base;

namespace Mealmate.Application.Models
{
    public class ChangePasswordModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string OPT { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password must matched")]
        public string ConfirmPassword { get; set; }
    }
}