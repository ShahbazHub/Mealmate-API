using Mealmate.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Mealmate.Admin.Controllers
{
    public class AuthController : Controller
    {
        public AuthController()
        {
        }

        #region SignUp
        [HttpPost]
        public IActionResult SignUp([Bind("SignUp")] LoginPageViewModel model)
        {
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion

        #region Sign In
        [HttpGet()]
        public IActionResult SignIn()
        {
            return View();
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
        [HttpPost]
        public IActionResult ForgetPassword([Bind(include: "Forget")] LoginPageViewModel model)
        {
            var forgetPasswordViewModel = model.Forget;

            return RedirectToAction("Auth", "SignIn");
        }
        #endregion

        #region Recover Password

        #endregion
    }
}
