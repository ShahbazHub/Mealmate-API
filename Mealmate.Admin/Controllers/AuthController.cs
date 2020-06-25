using Mealmate.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Mealmate.Admin.Controllers
{
    public class AuthController : Controller
    {
        public AuthController()
        {

        }

        #region Sign In
        [HttpGet()]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(SignInViewModel model)
        {
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Sign Out
        [HttpGet]
        public IActionResult SignOut()
        {
            return RedirectToAction("SignIn");
        }
        #endregion

        #region Forgot Password

        #endregion

        #region Recover Password

        #endregion
    }
}
