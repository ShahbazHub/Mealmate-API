using Mealmate.Admin.Areas.Admin.ViewModels;

using Microsoft.AspNetCore.Authorization;
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
            var model = new UserCreateViewModel()
            {
                Roles = new List<RoleAssignListViewModel>()
                {
                    new RoleAssignListViewModel()
                    {
                        AppRoleId = 1,
                        IsAssigned = false,
                        Name = "Admin"
                    },
                    new RoleAssignListViewModel()
                    {
                        AppRoleId = 2,
                        IsAssigned = false,
                        Name = "Operator"
                    },
                    new RoleAssignListViewModel()
                    {
                        AppRoleId = 1,
                        IsAssigned = false,
                        Name = "Waiter"
                    }
                }
            };
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

            model.Roles = new List<RoleAssignListViewModel>()
            {
                new RoleAssignListViewModel()
                {
                    AppRoleId = 1,
                    IsAssigned = false,
                    Name = "Admin"
                },
                new RoleAssignListViewModel()
                {
                    AppRoleId = 2,
                    IsAssigned = false,
                    Name = "Operator"
                },
                new RoleAssignListViewModel()
                {
                    AppRoleId = 1,
                    IsAssigned = false,
                    Name = "Waiter"
                }
            };

            return PartialView(model);
        }
        #endregion

        #region Update
        [HttpGet()]
        public IActionResult Update(int id)
        {
            var model = new UserUpdateViewModel()
            {
                AppUserId = id,
                Roles = new List<RoleAssignListViewModel>()
                {
                    new RoleAssignListViewModel()
                    {
                        AppRoleId = 1,
                        IsAssigned = false,
                        Name = "Admin"
                    },
                    new RoleAssignListViewModel()
                    {
                        AppRoleId = 2,
                        IsAssigned = false,
                        Name = "Operator"
                    },
                    new RoleAssignListViewModel()
                    {
                        AppRoleId = 1,
                        IsAssigned = false,
                        Name = "Waiter"
                    }
                }
            };

            return PartialView(model);
        }

        [HttpPost()]
        public IActionResult Update(UserUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            else
            {
                ModelState.AddModelError("", "Provide all valid data to proceed");
            }

            model.Roles = new List<RoleAssignListViewModel>()
            {
                new RoleAssignListViewModel()
                {
                    AppRoleId = 1,
                    IsAssigned = false,
                    Name = "Admin"
                },
                new RoleAssignListViewModel()
                {
                    AppRoleId = 2,
                    IsAssigned = false,
                    Name = "Operator"
                },
                new RoleAssignListViewModel()
                {
                    AppRoleId = 1,
                    IsAssigned = false,
                    Name = "Waiter"
                }
            };

            return PartialView(model);
        }
        #endregion

        #region Toggle
        [HttpDelete()]
        public IActionResult Toggle(int id)
        {
            bool Status = true;
            string Message = string.Empty;


            Message = "Record updated successfully";

            return Json(new { status = Status, message = Message });
        }
        #endregion
    }
}
