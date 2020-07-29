using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Controllers
{
    public class AuthController : Controller
    {
        public AuthController()
        {

        }

        [HttpGet()]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpGet()]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet()]
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
