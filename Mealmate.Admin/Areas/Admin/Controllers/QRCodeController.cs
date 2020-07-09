using Mealmate.Admin.Areas.Admin.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QRCodeController : Controller
    {
        public QRCodeController()
        {

        }

        #region Landing page
        public IActionResult Index()
        {
            var branches = new List<SelectListGroup>()
            {
                new SelectListGroup()
                {
                    Name = "Branch No. 1"
                },
                new SelectListGroup()
                {
                    Name = "Branch No. 2"
                },
                new SelectListGroup()
                {
                    Name = "Branch No. 3"
                }
            };

            var model = new QRCodeIndexViewModel()
            {
                Tables = new List<TableAssignListViewModel>()
                {
                    new TableAssignListViewModel()
                    {
                        TableId = 1,
                        Number = "T-1"
                    },
                    new TableAssignListViewModel()
                    {
                        TableId = 2,
                        Number = "T-2"
                    },
                    new TableAssignListViewModel()
                    {
                        TableId =3,
                        Number = "T-3"
                    },
                    new TableAssignListViewModel()
                    {
                        TableId = 4,
                        Number = "T-4"
                    },
                    new TableAssignListViewModel()
                    {
                        TableId = 5,
                        Number = "T-5"
                    },
                    new TableAssignListViewModel()
                    {
                        TableId = 6,
                        Number = "T-6"
                    }
                },
                Halls = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Group = branches[0],
                        Text = "Hall No. 1",
                        Value = "11"
                    },
                    new SelectListItem()
                    {
                        Group = branches[0],
                        Text = "Hall No. 2",
                        Value = "12"
                    },
                    new SelectListItem()
                    {
                        Group = branches[1],
                        Text = "Hall No. 1",
                        Value = "21"
                    },
                    new SelectListItem()
                    {
                        Group = branches[1],
                        Text = "Hall No. 2",
                        Value = "22"
                    },
                    new SelectListItem()
                    {
                        Group = branches[2],
                        Text = "Hall No. 1",
                        Value = "31"
                    },
                    new SelectListItem()
                    {
                        Group = branches[2],
                        Text = "Hall No. 2",
                        Value = "32"
                    }
                }
            };

            return View(model);
        }
        #endregion

        #region Read
        [HttpGet()]
        public IActionResult Detail(List<int> QRCodeTypes, List<int> Tables)
        {
            return ViewComponent("Mealmate.Admin.Areas.Admin.ViewComponents.QRCodeList",
                new { QRCodeTypes, Tables });
        }
        #endregion

        #region Create
        [HttpGet()]
        public IActionResult Create()
        {
            var branches = new List<SelectListGroup>()
            {
                new SelectListGroup()
                {
                    Name = "Branch No. 1"
                },
                new SelectListGroup()
                {
                    Name = "Branch No. 2"
                },
                new SelectListGroup()
                {
                    Name = "Branch No. 3"
                }
            };

            var model = new QRCodeCreateViewModel()
            {
                Tables = new List<TableAssignListViewModel>()
                {
                    new TableAssignListViewModel()
                    {
                        TableId = 1,
                        Number = "T-1"
                    },
                    new TableAssignListViewModel()
                    {
                        TableId = 2,
                        Number = "T-2"
                    },
                    new TableAssignListViewModel()
                    {
                        TableId =3,
                        Number = "T-3"
                    },
                    new TableAssignListViewModel()
                    {
                        TableId = 4,
                        Number = "T-4"
                    },
                    new TableAssignListViewModel()
                    {
                        TableId = 5,
                        Number = "T-5"
                    },
                    new TableAssignListViewModel()
                    {
                        TableId = 6,
                        Number = "T-6"
                    }
                },
                Halls = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Group = branches[0],
                        Text = "Hall No. 1",
                        Value = "11"
                    },
                    new SelectListItem()
                    {
                        Group = branches[0],
                        Text = "Hall No. 2",
                        Value = "12"
                    },
                    new SelectListItem()
                    {
                        Group = branches[1],
                        Text = "Hall No. 1",
                        Value = "21"
                    },
                    new SelectListItem()
                    {
                        Group = branches[1],
                        Text = "Hall No. 2",
                        Value = "22"
                    },
                    new SelectListItem()
                    {
                        Group = branches[2],
                        Text = "Hall No. 1",
                        Value = "31"
                    },
                    new SelectListItem()
                    {
                        Group = branches[2],
                        Text = "Hall No. 2",
                        Value = "32"
                    }
                }
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(QRCodeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { area = "Admin", controller = "QRCode" });
            }

            var branches = new List<SelectListGroup>()
            {
                new SelectListGroup()
                {
                    Name = "Branch No. 1"
                },
                new SelectListGroup()
                {
                    Name = "Branch No. 2"
                },
                new SelectListGroup()
                {
                    Name = "Branch No. 3"
                }
            };

            model.Tables = new List<TableAssignListViewModel>()
                {
                    new TableAssignListViewModel()
                    {
                        TableId = 1,
                        Number = "T-1"
                    },
                    new TableAssignListViewModel()
                    {
                        TableId = 2,
                        Number = "T-2"
                    },
                    new TableAssignListViewModel()
                    {
                        TableId =3,
                        Number = "T-3"
                    },
                    new TableAssignListViewModel()
                    {
                        TableId = 4,
                        Number = "T-4"
                    },
                    new TableAssignListViewModel()
                    {
                        TableId = 5,
                        Number = "T-5"
                    },
                    new TableAssignListViewModel()
                    {
                        TableId = 6,
                        Number = "T-6"
                    }
                };
            model.Halls = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Group = branches[0],
                        Text = "Hall No. 1",
                        Value = "11"
                    },
                    new SelectListItem()
                    {
                        Group = branches[0],
                        Text = "Hall No. 2",
                        Value = "12"
                    },
                    new SelectListItem()
                    {
                        Group = branches[1],
                        Text = "Hall No. 1",
                        Value = "21"
                    },
                    new SelectListItem()
                    {
                        Group = branches[1],
                        Text = "Hall No. 2",
                        Value = "22"
                    },
                    new SelectListItem()
                    {
                        Group = branches[2],
                        Text = "Hall No. 1",
                        Value = "31"
                    },
                    new SelectListItem()
                    {
                        Group = branches[2],
                        Text = "Hall No. 2",
                        Value = "32"
                    }
                };

            return View(model);
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
