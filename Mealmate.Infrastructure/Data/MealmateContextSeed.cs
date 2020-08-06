using Mealmate.Core.Entities;
using Mealmate.Core.Entities.Lookup;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.Infrastructure.Data
{
    public class MealmateContextSeed
    {
        private readonly MealmateContext _mealmateContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public MealmateContextSeed(
            MealmateContext mealmateContext,
            UserManager<User> userManager,
            RoleManager<Role> roleManager
            )
        {
            _mealmateContext = mealmateContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            //// TODO: Only run this if using a real database
            await _mealmateContext.Database.MigrateAsync();
            //await _mealmateContext.Database.EnsureCreatedAsync();

            // Users 
            await SeedUsersAsync();

            // Roles
            await SeedRolesAsync();

            // Lookups
            await SeedLookupsAsync();

        }

        #region Seeding Users
        private async Task SeedUsersAsync()
        {
            try
            {
                if (!_mealmateContext.Users.Any())
                {
                    var user = await _userManager.FindByEmailAsync("admin@gmail.com");
                    if (user == null)
                    {
                        user = new User
                        {
                            FirstName = "System",
                            LastName = "Administrator",
                            Email = "admin@gmail.com",
                            UserName = "admin@gmail.com"
                        };

                        var result = await _userManager.CreateAsync(user, "Server@123");
                        if (result != IdentityResult.Success)
                        {
                            throw new InvalidOperationException("Could not create user in Seeding");
                        }

                        _mealmateContext.Entry(user).State = EntityState.Unchanged;
                    }
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create role in Seeding");
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
                    var roles = new List<string>()
                    {
                        "SuperAdmin",
                        "RestaurantAdmin",
                        "FrontDesk",
                        "Waiter",
                        "Client"
                    };

                    foreach (var role in roles)
                    {
                        await _roleManager.CreateAsync(new Role() { Name = role, Created = DateTime.Now });
                    }
                }

            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not seed roles");
            }

        }
        #endregion

        #region Seeding Lookups
        private async Task SeedLookupsAsync()
        {
            try
            {
                if (!_mealmateContext.ContactRequestStates.Any())
                {
                    var contactRequests = new List<string>()
                    {
                        "Initited",
                        "Served",
                        "Cancelled"
                    };

                    foreach (var item in contactRequests)
                    {
                        await _mealmateContext.ContactRequestStates.AddAsync(
                            new ContactRequestState
                            {
                                Created = DateTime.Now,
                                IsActive = true,
                                Name = item
                            });
                    }
                }

                if (!_mealmateContext.RestroomRequestStates.Any())
                {
                    var restroomRequestStates = new List<string>()
                    {
                        "Initiated",
                        "Acknowledged",
                        "Served",
                        "Cancelled"
                    };

                    foreach (var item in restroomRequestStates)
                    {
                        await _mealmateContext.RestroomRequestStates.AddAsync(
                            new RestroomRequestState
                            {
                                Created = DateTime.Now,
                                IsActive = true,
                                Name = item
                            });
                    }
                }

                if (!_mealmateContext.CuisineTypes.Any())
                {
                    var cuisineTypes = new List<string>()
                    {
                        "Arabian",
                        "Chineese",
                        "Russian",
                        "Korean",
                        "German",
                        "Indian"
                    };

                    foreach (var item in cuisineTypes)
                    {
                        await _mealmateContext.CuisineTypes.AddAsync(
                            new CuisineType
                            {
                                Created = DateTime.Now,
                                IsActive = true,
                                Name = item
                            });
                    }
                }

                if (!_mealmateContext.OrderStates.Any())
                {
                    var orderStates = new List<string>()
                    {
                        "Cart",
                        "New",
                        "Acknowledged",
                        "Delivered",
                        "Bill Generated",
                        "Paid",
                        "Cancelled",
                    };

                    foreach (var item in orderStates)
                    {
                        await _mealmateContext.OrderStates.AddAsync(
                            new OrderState
                            {
                                Created = DateTime.Now,
                                IsActive = true,
                                Name = item
                            });
                    }
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Could not create lookups in Seeding");
            }

        }
        #endregion
    }
}
