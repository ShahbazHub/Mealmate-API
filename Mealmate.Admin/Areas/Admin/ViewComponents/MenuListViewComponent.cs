using Mealmate.Admin.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.ViewComponents
{
    public class MenuListViewComponent : ViewComponent
    {
        public MenuListViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(List<int> MenuTypes, List<int> Ingredients)
        {
            var items = await GetItemsAsync(MenuTypes, Ingredients);
            return View(items);
        }

        private Task<List<MenuListViewModel>> GetItemsAsync(List<int> MenuTypes, List<int> Ingredients)
        {
            var result = new List<MenuListViewModel>()
            {
                new MenuListViewModel()
                {
                    MenuId = 1,
                    Title = "Blueberry and banana french toast",
                    MenuTypeId = 1,
                    MenuType = "Breakfast",
                    MenuStateId = 1,
                    Branch = "Branch No. 1",
                    Location = "Location No. 1"
                },
                new MenuListViewModel()
                {
                    MenuId = 2,
                    Title = "Acai bowl with fresh seasonal fruit",
                    MenuTypeId = 2,
                    MenuType = "Lunch",
                    MenuStateId = 1,
                    Branch = "Branch No. 1",
                    Location = "Location No. 2"
                },
                new MenuListViewModel()
                {
                    MenuId = 3,
                    Title = "Avocado and italian plum tomatoes on toast",
                    MenuTypeId = 3,
                    MenuType = "Dinner",
                    MenuStateId = 1,
                    Branch = "Branch No. 2",
                    Location = "Location No. 1"
                },
                new MenuListViewModel()
                {
                    MenuId = 4,
                    Title = "Shakshuka",
                    MenuTypeId = 4,
                    MenuType = "Sides",
                    MenuStateId = 1,
                    Branch = "Branch No. 2",
                    Location = "Location No. 2"
                }
            };

            return Task.FromResult(result);
        }
    }
}
