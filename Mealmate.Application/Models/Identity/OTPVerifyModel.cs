using System;
using System.ComponentModel.DataAnnotations;
using Mealmate.Application.Models.Base;

namespace Mealmate.Application.Models
{
    public class OTPVerifyModel
    {
        [Required]
        public string Email{ get; set; }

        [Required]
        public string Otp { get; set; }
    }
}