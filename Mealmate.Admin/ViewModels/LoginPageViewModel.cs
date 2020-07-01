using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.ViewModels
{
    public class LoginPageViewModel
    {
        [BindProperty]
        public SignInViewModel SignIn { get; set; }
        [BindProperty]
        public SignUpViewModel SignUp { get; set; }
        [BindProperty]
        public ForgetPasswordViewModel Forget { get; set; }

        
    }
}
