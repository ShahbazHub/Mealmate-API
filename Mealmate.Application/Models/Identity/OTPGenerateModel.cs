using System;
using System.ComponentModel.DataAnnotations;
using Mealmate.Application.Models.Base;

namespace Mealmate.Application.Models
{
    public class OTPGenerateModel
    {
        [Required]
        public string Email{ get; set; }
    }
}