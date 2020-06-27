using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Setting.Controllers
{
    [Area("Setting")]
    public class RestaurantController : Controller
    {
        public RestaurantController()
        {

        }

        #region Landing Page
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Read
        [HttpGet()]
        public IActionResult Detail()
        {
            return ViewComponent("Mealmate.Admin.Areas.Setting.ViewComponents.RestaurantList");
        }
        #endregion
    }
}
