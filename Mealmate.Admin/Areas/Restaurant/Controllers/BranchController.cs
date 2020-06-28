using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Restaurant.Controllers
{
    [Area("Restaurant")]
    public class BranchController : Controller
    {
        public BranchController()
        {

        }

        #region Branch Home
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Read
        [HttpGet()]
        public IActionResult Detail()
        {
            return ViewComponent("Mealmate.Admin.Areas.Restaurant.ViewComponents.BranchList");
        }
        #endregion

        #region Create
        [HttpGet()]
        public IActionResult Create()
        {
            return View();
        }
        #endregion
    }
}
