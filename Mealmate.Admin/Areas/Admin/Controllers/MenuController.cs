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
    }
}
