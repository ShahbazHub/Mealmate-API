using Mealmate.Admin.Areas.Admin.ViewModels;
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
            var model = new MenuIndexViewModel()
            {
                Ingredients = new List<IngredientAssignListViewModel>()
                {
                    new IngredientAssignListViewModel()
                    {
                        IngredientId = 1,
                        Name = "Egg"
                    },
                    new IngredientAssignListViewModel()
                    {
                        IngredientId = 2,
                        Name = "Fish"
                    },
                    new IngredientAssignListViewModel()
                    {
                        IngredientId = 3,
                        Name = "Lupin"
                    },
                    new IngredientAssignListViewModel()
                    {
                        IngredientId = 4,
                        Name = "Milk"
                    },
                    new IngredientAssignListViewModel()
                    {
                        IngredientId = 5,
                        Name = "Mustard"
                    },
                    new IngredientAssignListViewModel()
                    {
                        IngredientId = 1,
                        Name = "Peanut"
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
            var restaurants = new List<SelectListGroup>()
            {
                new SelectListGroup()
                {
                    Name = "Restaurant No. 1"
                },
                new SelectListGroup()
                {
                    Name = "Restaurant No. 2"
                },
                new SelectListGroup()
                {
                    Name = "Restaurant No. 3"
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
                Halls = new List<SelectListItem>()
                {
                    new SelectListItem()
                    {
                        Group = restaurants[0],
                        Text = "Hall No. 1",
                        Value = "11"
                    },
                    new SelectListItem()
                    {
                        Group = restaurants[0],
                        Text = "Hall No. 2",
                        Value = "12"
                    },
                    new SelectListItem()
                    {
                        Group = restaurants[1],
                        Text = "Hall No. 1",
                        Value = "21"
                    },
                    new SelectListItem()
                    {
                        Group = restaurants[1],
                        Text = "Hall No. 2",
                        Value = "22"
                    },
                    new SelectListItem()
                    {
                        Group = restaurants[2],
                        Text = "Hall No. 1",
                        Value = "31"
                    },
                    new SelectListItem()
                    {
                        Group = restaurants[2],
                        Text = "Hall No. 2",
                        Value = "32"
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

            var restaurants = new List<SelectListGroup>()
            {
                new SelectListGroup()
                {
                    Name = "Restaurant No. 1"
                },
                new SelectListGroup()
                {
                    Name = "Restaurant No. 2"
                },
                new SelectListGroup()
                {
                    Name = "Restaurant No. 3"
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

            model.Halls = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Group = restaurants[0],
                    Text = "Hall No. 1",
                    Value = "11"
                },
                new SelectListItem()
                {
                    Group = restaurants[0],
                    Text = "Hall No. 2",
                    Value = "12"
                },
                new SelectListItem()
                {
                    Group = restaurants[1],
                    Text = "Hall No. 1",
                    Value = "21"
                },
                new SelectListItem()
                {
                    Group = restaurants[1],
                    Text = "Hall No. 2",
                    Value = "22"
                },
                new SelectListItem()
                {
                    Group = restaurants[2],
                    Text = "Hall No. 1",
                    Value = "31"
                },
                new SelectListItem()
                {
                    Group = restaurants[2],
                    Text = "Hall No. 2",
                    Value = "32"
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
                    Quantity = 0,
                    Ingredients = new List<SelectListItem>()
                    {
                        new SelectListItem()
                        {
                            Text = "Egg",
                            Value = "1"
                        },
                        new SelectListItem()
                        {
                            Text = "Fish",
                            Value = "2"
                        },
                        new SelectListItem()
                        {
                            Text = "Lupin",
                            Value = "3"
                        },
                        new SelectListItem()
                        {
                            Text = "Milk",
                            Value = "4"
                        },
                        new SelectListItem()
                        {
                            Text = "Mustard",
                            Value = "5"
                        },
                        new SelectListItem()
                        {
                            Text = "Peanut",
                            Value = "6"
                        }
                    }
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
