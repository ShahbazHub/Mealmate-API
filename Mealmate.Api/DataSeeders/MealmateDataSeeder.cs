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

                    var path = Path.Combine(_env.ContentRootPath, "Data/allergens-svg-data.json");
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

                    var path = Path.Combine(_env.ContentRootPath, "Data/dietaries-svg-data.json");
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
    }
}
