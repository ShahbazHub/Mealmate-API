using Mealmate.Admin.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Admin.Areas.Admin.ViewComponents
{
    public class UserListViewComponent : ViewComponent
    {
        public UserListViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync(string username, string email)
        {
            var items = await GetItemsAsync(username, email);
            return View(items);
        }

        private Task<List<UserListViewModel>> GetItemsAsync(string username, string email)
        {
            var result = new List<UserListViewModel>()
            {
                new UserListViewModel()
                {
                    AppUserId = 1,
                    Email = "admin1234@gmail.com",
                    Name = "Muhammad Ali",
                    Phone = "",
                    Username = "admin1234",
                    StateId = 1,
                    Roles = 1
                },
                new UserListViewModel()
                {
                    AppUserId = 2,
                    Email = "johndoe@gmail.com",
                    Name = "John Doe",
                    Phone = "",
                    Username = "johndoe",
                    StateId = 2,
                    Roles = 1
                },
                new UserListViewModel()
                {
                    AppUserId = 2,
                    Email = "tahirsaleem@gmail.com",
                    Name = "Tahir Saleem",
                    Phone = "",
                    Username = "tahir1985",
                    StateId = 1,
                    Roles = 1
                }
            };

            if (result != null)
            {
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrWhiteSpace(username))
                {
                    result = result.Where(p => p.Name
                                                .ToLower()
                                                .Contains(username.ToLower()))
                                    .ToList();
                }

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrWhiteSpace(email))
                {
                    result = result.Where(p => p.Email
                                                .ToLower()
                                                .Contains(email.ToLower()))
                                    .ToList();
                }

            }

            return Task.FromResult(result);
        }
    }
}
