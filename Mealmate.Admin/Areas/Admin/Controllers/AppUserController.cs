using Mealmate.Admin.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppUserController : Controller
    {
        public AppUserController()
        {

        }

        #region Landing page
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Read
        [HttpGet()]
        public IActionResult Detail()
        {
            return ViewComponent("Mealmate.Admin.Areas.Admin.ViewComponents.UserList");
        }
        #endregion

        #region Create
        [HttpGet()]
        public IActionResult Create()
        {
            var model = new UserCreateViewModel();
            return PartialView(model);
        }

        [HttpPost()]
        public IActionResult Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            else
            {
                ModelState.AddModelError("", "Provide all valid data to proceed");
            }

            return PartialView(model);
        }
        #endregion
    }
}
