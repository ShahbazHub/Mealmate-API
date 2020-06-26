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
    }
}
