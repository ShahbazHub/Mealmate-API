using Mealmate.Core.Entities;
using Mealmate.Core.Entities.Lookup;
using Mealmate.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Api.DataSeeders
{
    public class MealmateDataSeeder
    {
        private readonly IWebHostEnvironment _env;
        private readonly MealmateContext _mealmateContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public MealmateDataSeeder(
            MealmateContext mealmateContext,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IWebHostEnvironment env)
        {
            _env = env;
            _mealmateContext = mealmateContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            //// TODO: Only run this if using a real database
            await _mealmateContext.Database.MigrateAsync();
            await _mealmateContext.Database.EnsureCreatedAsync();

            // Users 
            await SeedUsersAsync();

            // Roles
            await SeedRolesAsync();

            // Lookups
            await SeedCuisineTypesAsync();
            await SeedAllergensAsync();
            await SeedDietariesAsync();
            await SeedContactRequestStatesAsync();
            await SeedRestroomRequestStatesAsync();
            await SeedOrderStatesAsync();
            await SeedBillRequestStatesAsync();
            await SeedRestaurantsAsync();
            await SeedBranchesAsync();
            await SeedMenusAsync();
            await SeedMenuItemsAsync();
            await SeedMenuItemAllergensAsync();
            await SeedMenuItemDietariesAsync();
        }

        #region Seeding Users
        private async Task SeedUsersAsync()
        {
            try
            {
                if (!_userManager.Users.Any())
                {

                    var path = Path.Combine(_env.ContentRootPath, "Data/users-data.json");
                    var file = await File.ReadAllTextAsync(path);

                    var result = JsonConvert.DeserializeObject<IEnumerable<User>>(file);
                    if (result != null)
                    {
                        foreach (var item in result)
                        {
                            var user = new User
                            {
                                FirstName = item.FirstName,
                                LastName = item.LastName,
                                Email = item.Email,
                                UserName = item.UserName,
                                PhoneNumber = item.PhoneNumber
                            };

                            var resultUser = await _userManager.CreateAsync(user, "password");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create users in Seeding");
            }
        }
        #endregion

        #region Seeding Roles
        private async Task SeedRolesAsync()
        {
            try
            {
                if (!_mealmateContext.Roles.Any())
                {

                    var path = Path.Combine(_env.ContentRootPath, "Data/roles-data.json");
                    var file = await File.ReadAllTextAsync(path);

                    var result = JsonConvert.DeserializeObject<IEnumerable<Role>>(file);
                    if (result != null)
                    {
                        foreach (var item in result)
                        {
                            var role = new Role
                            {
                                Created = DateTime.Now,
                                Name = item.Name,
                                NormalizedName = item.Name.ToUpper()
                            };
                            await _mealmateContext.AddAsync(role);
                        }
                    }
                    await _mealmateContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create roles in Seeding");
            }
        }
        #endregion

        #region Seeding Lookups
        private async Task SeedCuisineTypesAsync()
        {
            try
            {
                if (!_mealmateContext.CuisineTypes.Any())
                {

                    var path = Path.Combine(_env.ContentRootPath, "Data/cuisinetypes-data.json");
                    var file = await File.ReadAllTextAsync(path);

                    var result = JsonConvert.DeserializeObject<IEnumerable<CuisineType>>(file);
                    if (result != null)
                    {
                        await _mealmateContext.AddRangeAsync(result);
                    }
                    await _mealmateContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create roles in Seeding");
            }
        }

        private async Task SeedAllergensAsync()
        {
            try
            {
                if (!_mealmateContext.Allergens.Any())
                {

                    var path = Path.Combine(_env.ContentRootPath, "Data/allergens-data.json");
                    var file = await File.ReadAllTextAsync(path);

                    var result = JsonConvert.DeserializeObject<IEnumerable<Allergen>>(file);
                    if (result != null)
                    {
                        await _mealmateContext.AddRangeAsync(result);
                    }
                    await _mealmateContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create allergens in Seeding");
            }
        }

        private async Task SeedDietariesAsync()
        {
            try
            {
                if (!_mealmateContext.Dietaries.Any())
                {

                    var path = Path.Combine(_env.ContentRootPath, "Data/dietaries-data.json");
                    var file = await File.ReadAllTextAsync(path);

                    var result = JsonConvert.DeserializeObject<IEnumerable<Dietary>>(file);
                    if (result != null)
                    {
                        await _mealmateContext.AddRangeAsync(result);
                    }
                    await _mealmateContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create allergens in Seeding");
            }
        }

        private async Task SeedContactRequestStatesAsync()
        {
            try
            {
                if (!_mealmateContext.ContactRequestStates.Any())
                {

                    var path = Path.Combine(_env.ContentRootPath, "Data/contactrequeststates-data.json");
                    var file = await File.ReadAllTextAsync(path);

                    var result = JsonConvert.DeserializeObject<IEnumerable<ContactRequestState>>(file);
                    if (result != null)
                    {
                        await _mealmateContext.AddRangeAsync(result);
                    }
                    await _mealmateContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create contact request state in Seeding");
            }
        }

        private async Task SeedRestroomRequestStatesAsync()
        {
            try
            {
                if (!_mealmateContext.RestroomRequestStates.Any())
                {

                    var path = Path.Combine(_env.ContentRootPath, "Data/restroomrequeststates-data.json");
                    var file = await File.ReadAllTextAsync(path);

                    var result = JsonConvert.DeserializeObject<IEnumerable<RestroomRequestState>>(file);
                    if (result != null)
                    {
                        await _mealmateContext.AddRangeAsync(result);
                    }
                    await _mealmateContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create restroom request state in Seeding");
            }
        }

        private async Task SeedOrderStatesAsync()
        {
            try
            {
                if (!_mealmateContext.OrderStates.Any())
                {

                    var path = Path.Combine(_env.ContentRootPath, "Data/orderststates-data.json");
                    var file = await File.ReadAllTextAsync(path);

                    var result = JsonConvert.DeserializeObject<IEnumerable<OrderState>>(file);
                    if (result != null)
                    {
                        await _mealmateContext.AddRangeAsync(result);
                    }
                    await _mealmateContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create restroom request state in Seeding");
            }
        }

        private async Task SeedBillRequestStatesAsync()
        {
            try
            {
                if (!_mealmateContext.BillRequestStates.Any())
                {

                    var path = Path.Combine(_env.ContentRootPath, "Data/billrequestststates-data.json");
                    var file = await File.ReadAllTextAsync(path);

                    var result = JsonConvert.DeserializeObject<IEnumerable<BillRequestState>>(file);
                    if (result != null)
                    {
                        await _mealmateContext.AddRangeAsync(result);
                    }
                    await _mealmateContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create bill request state in Seeding");
            }
        }
        #endregion

        #region Seeding Restaurants Data
        private async Task SeedRestaurantsAsync()
        {
            try
            {
                if (!_mealmateContext.Restaurants.Any())
                {
                    var restaurantPath = Path.Combine(_env.ContentRootPath, "Data/restaurants-data.json");
                    var restaurantFile = await File.ReadAllTextAsync(restaurantPath);

                    var restaurants = JsonConvert.DeserializeObject<IEnumerable<Restaurant>>(restaurantFile);
                    if (restaurants != null)
                    {
                        foreach (var restaurant in restaurants)
                        {
                            restaurant.Created = DateTime.Now;
                            var resultRestaurant = await _mealmateContext.Restaurants.AddAsync(restaurant);
                            if (await _mealmateContext.SaveChangesAsync() > 0)
                            {
                                var user = await _mealmateContext.Users.FirstOrDefaultAsync(p => p.Email.ToLower() == "admin@gmail.com");
                                if (user != null)
                                {
                                    var userRestaurant = new UserRestaurant
                                    {
                                        Created = DateTime.Now,
                                        IsActive = true,
                                        isOwner = true,
                                        RestaurantId = restaurant.Id,
                                        UserId = user.Id
                                    };

                                    await _mealmateContext.UserRestaurants.AddAsync(userRestaurant);
                                    await _mealmateContext.SaveChangesAsync();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create restaurants in Seeding");
            }
        }
        #endregion

        #region Seeding Branches Data
        private async Task SeedBranchesAsync()
        {
            try
            {
                if (_mealmateContext.Restaurants.Any() && !_mealmateContext.Branches.Any())
                {
                    var restaurants = await _mealmateContext.Restaurants.ToListAsync();
                    if (restaurants != null)
                    {
                        foreach (var restaurant in restaurants)
                        {
                            var branchPath = Path.Combine(_env.ContentRootPath, "Data/branches-data.json");
                            var branchFile = await File.ReadAllTextAsync(branchPath);

                            var branches = JsonConvert.DeserializeObject<IEnumerable<Branch>>(branchFile);
                            if (branches != null)
                            {
                                foreach (var branch in branches)
                                {
                                    branch.RestaurantId = restaurant.Id;
                                    branch.Created = DateTime.Now;

                                    var resultBranch = await _mealmateContext.Branches.AddAsync(branch);
                                    await _mealmateContext.SaveChangesAsync();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create branches in Seeding");
            }
        }
        #endregion

        #region Seeding Menus Data
        private async Task SeedMenusAsync()
        {
            try
            {
                if (_mealmateContext.Branches.Any() && !_mealmateContext.Menus.Any())
                {
                    var branches = await _mealmateContext.Branches.ToListAsync();
                    if (branches != null)
                    {
                        foreach (var branch in branches)
                        {
                            var menuPath = Path.Combine(_env.ContentRootPath, "Data/menus-data.json");
                            var menuFile = await File.ReadAllTextAsync(menuPath);

                            var menus = JsonConvert.DeserializeObject<IEnumerable<Menu>>(menuFile);
                            if (menus != null)
                            {
                                foreach (var menu in menus)
                                {
                                    menu.BranchId = branch.Id;
                                    branch.Created = DateTime.Now;

                                    var resultMenu = await _mealmateContext.Menus.AddAsync(menu);
                                    await _mealmateContext.SaveChangesAsync();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create menus in Seeding");
            }
        }
        #endregion

        #region Seeding Menu Items Data
        private async Task SeedMenuItemsAsync()
        {
            try
            {
                if (_mealmateContext.Menus.Any() && !_mealmateContext.MenuItems.Any())
                {
                    var menus = await _mealmateContext.Menus.ToListAsync();
                    if (menus != null)
                    {
                        foreach (var menu in menus)
                        {
                            var menuItemPath = Path.Combine(_env.ContentRootPath, "Data/menuitems-data.json");
                            var menuItemFile = await File.ReadAllTextAsync(menuItemPath);

                            var menuItems = JsonConvert.DeserializeObject<IEnumerable<MenuItem>>(menuItemFile);
                            if (menuItems != null)
                            {
                                var cuisineTypes = await _mealmateContext.CuisineTypes.ToListAsync();
                                if (cuisineTypes != null)
                                {
                                    foreach (var cuisineType in cuisineTypes)
                                    {
                                        foreach (var menuItem in menuItems)
                                        {
                                            var entity = new MenuItem
                                            {
                                                MenuId = menu.Id,
                                                CuisineTypeId = cuisineType.Id,
                                                Created = DateTime.Now,
                                                Description = menuItem.Description,
                                                IsActive = menuItem.IsActive,
                                                Name = menuItem.Name,
                                                Photo = menuItem.Photo,
                                                Price = menuItem.Price,
                                            };

                                            var resultMenu = await _mealmateContext.MenuItems.AddAsync(entity);
                                            await _mealmateContext.SaveChangesAsync();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create menus in Seeding");
            }
        }
        #endregion

        #region Seeding Menu Allergens
        private async Task SeedMenuItemAllergensAsync()
        {
            try
            {
                if (!_mealmateContext.MenuItemAllergens.Any())
                {
                    var menuItems = await _mealmateContext.MenuItems.ToListAsync();
                    var allergens = await _mealmateContext.Allergens.ToListAsync();
                    if (menuItems != null && allergens != null)
                    {
                        foreach (var menuItem in menuItems)
                        {
                            foreach (var allergen in allergens)
                            {
                                var rnd = new Random().Next(1, 100);
                                if (rnd % 2 == 0)
                                {
                                    var menuItemAllergen = new MenuItemAllergen
                                    {
                                        AllergenId = allergen.Id,
                                        Created = DateTime.Now,
                                        IsActive = true,
                                        MenuItemId = menuItem.Id
                                    };

                                    await _mealmateContext.MenuItemAllergens.AddAsync(menuItemAllergen);
                                    await _mealmateContext.SaveChangesAsync();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create roles in Seeding");
            }
        }
        #endregion

        #region Seeding Menu Dietariess
        private async Task SeedMenuItemDietariesAsync()
        {
            try
            {
                if (!_mealmateContext.MenuItemDietaries.Any())
                {
                    var menuItems = await _mealmateContext.MenuItems.ToListAsync();
                    var dietaries = await _mealmateContext.Dietaries.ToListAsync();
                    if (menuItems != null && dietaries != null)
                    {
                        foreach (var menuItem in menuItems)
                        {
                            foreach (var dietary in dietaries)
                            {
                                var rnd = new Random().Next(1, 100);
                                if (rnd % 2 == 0)
                                {
                                    var menuItemDietaries = new MenuItemDietary
                                    {
                                        DietaryId = dietary.Id,
                                        Created = DateTime.Now,
                                        IsActive = true,
                                        MenuItemId = menuItem.Id
                                    };

                                    await _mealmateContext.MenuItemDietaries.AddAsync(menuItemDietaries);
                                    await _mealmateContext.SaveChangesAsync();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create roles in Seeding");
            }
        }
        #endregion
    }
}
