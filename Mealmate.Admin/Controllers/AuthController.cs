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

        [HttpPost]
        public IActionResult SignIn(LoginRequest request)
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult SignOut()
        {
            return RedirectToAction("SignIn");
        }

    }
}
