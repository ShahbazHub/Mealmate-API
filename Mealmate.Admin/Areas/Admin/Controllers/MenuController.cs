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
    public class MenuController : Controller
    {
        public MenuController()
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

            var model = new MenuIndexViewModel()
            {

                Locations = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Group = branches[0],
                        Text = "Location No. 1",
                        Value = "11"
                    },
                    new SelectListItem()
                    {
                        Group = branches[0],
                        Text = "Location No. 2",
                        Value = "12"
                    },
                    new SelectListItem()
                    {
                        Group = branches[1],
                        Text = "Location No. 1",
                        Value = "21"
                    },
                    new SelectListItem()
                    {
                        Group = branches[1],
                        Text = "Location No. 2",
                        Value = "22"
                    },
                    new SelectListItem()
                    {
                        Group = branches[2],
                        Text = "Location No. 1",
                        Value = "31"
                    },
                    new SelectListItem()
                    {
                        Group = branches[2],
                        Text = "Location No. 2",
                        Value = "32"
                    }
                },
                MenuTypes = new List<MenuTypeAssignListViewModel>()
                {
                    new MenuTypeAssignListViewModel()
                    {
                        Name = "Breakfast",
                        MenuTypeId = 1
                    },
                    new MenuTypeAssignListViewModel()
                    {
                        Name = "Lunch",
                        MenuTypeId = 2
                    },
                    new MenuTypeAssignListViewModel()
                    {
                        Name = "Dinner",
                        MenuTypeId = 3
                    },
                    new MenuTypeAssignListViewModel()
                    {
                        Name = "Sides",
                        MenuTypeId = 4
                    },
                    new MenuTypeAssignListViewModel()
                    {
                        Name = "Children's",
                        MenuTypeId = 5
                    },
                    new MenuTypeAssignListViewModel()
                    {
                        Name = "Desserts",
                        MenuTypeId = 6
                    },
                    new MenuTypeAssignListViewModel()
                    {
                        Name = "Drinks",
                        MenuTypeId = 7
                    }
                }
            };
            return View(model);
        }
        #endregion

        #region Read
        [HttpGet()]
        public IActionResult Detail(List<int> MenuTypes, List<int> Ingredients)
        {
            return ViewComponent("Mealmate.Admin.Areas.Admin.ViewComponents.MenuList",
                new { MenuTypes, Ingredients });
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

            var model = new MenuCreateViewModel()
            {
                
                MenuTypes = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Text = "Breakfast",
                        Value = "1"
                    },
                    new SelectListItem()
                    {
                        Text = "Lunch",
                        Value = "2"
                    },
                    new SelectListItem()
                    {
                        Text = "Dinner",
                        Value = "3"
                    },
                    new SelectListItem()
                    {
                        Text = "Sides",
                        Value = "4"
                    }
                },
                Locations = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Group = branches[0],
                        Text = "Location No. 1",
                        Value = "11"
                    },
                    new SelectListItem()
                    {
                        Group = branches[0],
                        Text = "Location No. 2",
                        Value = "12"
                    },
                    new SelectListItem()
                    {
                        Group = branches[1],
                        Text = "Location No. 1",
                        Value = "21"
                    },
                    new SelectListItem()
                    {
                        Group = branches[1],
                        Text = "Location No. 2",
                        Value = "22"
                    },
                    new SelectListItem()
                    {
                        Group = branches[2],
                        Text = "Location No. 1",
                        Value = "31"
                    },
                    new SelectListItem()
                    {
                        Group = branches[2],
                        Text = "Location No. 2",
                        Value = "32"
                    }
                },
                Dietaries = new List<DietaryAssignListViewModel>()
                {
                    new DietaryAssignListViewModel()
                    {
                        DietaryId = 1,
                        Name = "Gluten Free"
                    },
                    new DietaryAssignListViewModel()
                    {
                        DietaryId = 2,
                        Name = "Celiac"
                    },
                    new DietaryAssignListViewModel()
                    {
                        DietaryId = 3,
                        Name = "Vegetarian"
                    },
                    new DietaryAssignListViewModel()
                    {
                        DietaryId = 4,
                        Name = "Vegan"
                    }
                },
                Allergens = new List<AllergenAssignListViewModel>()
                {
                    new AllergenAssignListViewModel()
                    {
                        AllergenId = 1,
                        Name = "Egg"
                    },
                    new AllergenAssignListViewModel()
                    {
                        AllergenId = 2,
                        Name = "Celery"
                    },
                    new AllergenAssignListViewModel()
                    {
                        AllergenId = 3,
                        Name = "Fish"
                    },
                    new AllergenAssignListViewModel()
                    {
                        AllergenId = 4,
                        Name = "Lupin"
                    },
                    new AllergenAssignListViewModel()
                    {
                        AllergenId = 5,
                        Name = "Milk"
                    }
                }
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MenuCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", new { area = "Admin", controller = "Menu" });
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
            model.MenuTypes = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Text = "Breakfast",
                    Value = "1"
                },
                new SelectListItem()
                {
                    Text = "Lunch",
                    Value = "2"
                },
                new SelectListItem()
                {
                    Text = "Dinner",
                    Value = "3"
                },
                new SelectListItem()
                {
                    Text = "Sides",
                    Value = "4"
                }
            };

            model.Locations = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Group = branches[0],
                    Text = "Location No. 1",
                    Value = "11"
                },
                new SelectListItem()
                {
                    Group = branches[0],
                    Text = "Location No. 2",
                    Value = "12"
                },
                new SelectListItem()
                {
                    Group = branches[1],
                    Text = "Location No. 1",
                    Value = "21"
                },
                new SelectListItem()
                {
                    Group = branches[1],
                    Text = "Location No. 2",
                    Value = "22"
                },
                new SelectListItem()
                {
                    Group = branches[2],
                    Text = "Location No. 1",
                    Value = "31"
                },
                new SelectListItem()
                {
                    Group = branches[2],
                    Text = "Location No. 2",
                    Value = "32"
                }
            };

            model.Dietaries = new List<DietaryAssignListViewModel>()
            {
                new DietaryAssignListViewModel()
                {
                    DietaryId = 1,
                    Name = "Gluten Free"
                },
                new DietaryAssignListViewModel()
                {
                    DietaryId = 2,
                    Name = "Celiac"
                },
                new DietaryAssignListViewModel()
                {
                    DietaryId = 3,
                    Name = "Vegetarian"
                },
                new DietaryAssignListViewModel()
                {
                    DietaryId = 4,
                    Name = "Vegan"
                }
            };

            model.Allergens = new List<AllergenAssignListViewModel>()
            {
                new AllergenAssignListViewModel()
                {
                    AllergenId = 1,
                    Name = "Egg"
                },
                new AllergenAssignListViewModel()
                {
                    AllergenId = 2,
                    Name = "Celery"
                },
                new AllergenAssignListViewModel()
                {
                    AllergenId = 3,
                    Name = "Fish"
                },
                new AllergenAssignListViewModel()
                {
                    AllergenId = 4,
                    Name = "Lupin"
                },
                new AllergenAssignListViewModel()
                {
                    AllergenId = 5,
                    Name = "Milk"
                }
            };
            return View();
        }
        #endregion

        #region Menu Item Generation
        [HttpGet()]
        public IActionResult MenuItem()
        {
            return PartialView("MenuItem",
                new MenuItemCreateViewModel()
                {
                    Price = 0,
                    Quantity = 0
                });
        }
        #endregion

        #region Update
        [HttpGet()]
        public IActionResult Update(int id)
        {
            var model = new MenuUpdateViewModel()
            {
                MenuId = id,
                MenuTypes = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Text = "Breakfast",
                        Value = "1"
                    },
                    new SelectListItem()
                    {
                        Text = "Lunch",
                        Value = "2"
                    },
                    new SelectListItem()
                    {
                        Text = "Dinner",
                        Value = "3"
                    },
                    new SelectListItem()
                    {
                        Text = "Sides",
                        Value = "4"
                    }
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
