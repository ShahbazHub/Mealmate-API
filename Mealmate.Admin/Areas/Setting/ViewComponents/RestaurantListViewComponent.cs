using Mealmate.Admin.Areas.Admin.ViewModels;
using Mealmate.Admin.Areas.Setting.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Setting.ViewComponents
{
    public class RestaurantListViewComponent : ViewComponent
    {
        public RestaurantListViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await GetItemsAsync();
            return View(items);
        }

        private Task<List<RestaurantListViewModel>> GetItemsAsync()
        {
            var result = new List<RestaurantListViewModel>()
            {
                new RestaurantListViewModel()
                {
                    RestaurantId = 1,
                    Address = "Saddar Rawalpindi, Pakistan",
                    Email = "restaurant1@domain.com",
                    Halls = 3,
                    Name = "Restaurant No. 1",
                    Phone = "123456789",
                    StateId = 1,
                    State = "Active"
                },
                new RestaurantListViewModel()
                {
                    RestaurantId = 2,
                    Address = "Blue Area Islamabad, Pakistan",
                    Email = "restaurant2@domain.com",
                    Halls = 2,
                    Name = "Restaurant No. 2",
                    Phone = "987654321",
                    StateId = 1,
                    State = "Active"
                }
            };

            return Task.FromResult(result);
        }
    }
}
