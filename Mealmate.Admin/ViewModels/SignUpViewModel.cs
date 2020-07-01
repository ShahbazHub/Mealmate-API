using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.ViewModels
{
    public class SignUpViewModel
    {
        public string RestaurantName { get; set; }
        public string Owner { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public bool Agreed { get; set; }
    }
}
