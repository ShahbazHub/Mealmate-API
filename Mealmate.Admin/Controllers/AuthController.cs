using Mealmate.Admin.ViewModels;
using Mealmate.BusinessLayer.UnitOfWork;
using Mealmate.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Mealmate.Admin.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region SignUp
        [HttpPost]
        public async Task<IActionResult> SignUp([Bind("SignUp")] LoginPageViewModel model)
        {
            var signUpViewModel = model.SignUp;
            var result = await _userManager.FindByEmailAsync(signUpViewModel.Email);
            try
            {
                //var result = await _userManager.CreateAsync(new Entities.Identity.User
                //{
                //    Email = signUpViewModel.Email,
                //    Name = signUpViewModel.Owner,
                //    UserName = signUpViewModel.Email,
                //    EmailConfirmed = true

                //}, signUpViewModel.Password);

                //if (result.Succeeded)
                //{
                //    await _signInManager.PasswordSignInAsync(signUpViewModel.Email, signUpViewModel.Password, false, false);

                //    return RedirectToActionPermanent("Index", "Home");
                //}

            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion

        #region Sign In
        [HttpGet()]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([Bind(include: "SignIn")] LoginPageViewModel model)
        {
            var signInViewModel = model.SignIn;

            var result = await _signInManager.PasswordSignInAsync(signInViewModel.Username, signInViewModel.Password, signInViewModel.Remember, false);
            if (result.Succeeded)
            {
                return RedirectToActionPermanent("Index", "Home");
            }
            return RedirectToAction("SignIn", "Auth");
        }
        #endregion

        #region Sign Out
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
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
