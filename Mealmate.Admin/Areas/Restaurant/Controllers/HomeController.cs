using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Restaurant.Controllers
{
    [Area("Restaurant")]
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        #region Restaurant Home
        public IActionResult Index()
        {
            return View();
        }
        #endregion
    }
}
