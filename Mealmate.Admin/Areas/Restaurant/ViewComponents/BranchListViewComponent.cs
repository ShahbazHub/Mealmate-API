using Mealmate.Admin.Areas.Admin.ViewModels;
using Mealmate.Admin.Areas.Restaurant.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Restaurant.ViewComponents
{
    public class BranchListViewComponent : ViewComponent
    {
        public BranchListViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }

        private Task<List<BranchListViewModel>> GetItemsAsync()
        {
            var result = new List<BranchListViewModel>()
            {
                new BranchListViewModel()
                {
                    BranchId = 1,
                    Address = "Saddar Rawalpindi, Pakistan",
                    Email = "restaurant1@domain.com",
                    Halls = 3,
                    Name = "Branch No. 1",
                    Phone = "123456789",
                    StateId = 1,
                    State = "Active"
                },
                new BranchListViewModel()
                {
                    BranchId = 2,
                    Address = "Blue Area Islamabad, Pakistan",
                    Email = "restaurant2@domain.com",
                    Halls = 2,
                    Name = "Branch No. 2",
                    Phone = "987654321",
                    StateId = 1,
                    State = "Active"
                }
            };

            return Task.FromResult(result);
        }
    }
}
