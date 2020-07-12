using Mealmate.Core.Entities;
using Mealmate.Core.Repositories;
using System;
using Mealmate.Core.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Mealmate.Infrastructure.Data
{
    public class MealmateContextSeed
    {
        private readonly MealmateContext _mealmateContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        //private readonly IRestaurantRepository _restaurantRepository;
        //private readonly IRepository<Table> _tableRepository;

        public MealmateContextSeed(
            MealmateContext mealmateContext,
            UserManager<User> userManager,
            RoleManager<Role> roleManager
            //IRestaurantRepository restaurantRepository,
            //IRepository<Table> tableRepository
            )
        {
            _mealmateContext = mealmateContext;
            _userManager = userManager;
            _roleManager = roleManager;
            //_restaurantRepository = restaurantRepository;
            //_tableRepository = tableRepository;
        }

        public async Task SeedAsync()
        {
            //// TODO: Only run this if using a real database
            //await _mealmateContext.Database.MigrateAsync();
            //await _mealmateContext.Database.EnsureCreatedAsync();

            //// users
            await SeedUsersAsync();
            await SeedRolesAsync();

        }
        private async Task SeedUsersAsync()
        {
            var user = await _userManager.FindByEmailAsync("admin@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "System",
                    LastName = "Administrator",
                    Email = "admin@gmail.com",
                    UserName = "admin"
                };

                var result = await _userManager.CreateAsync(user, "Server@123");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create user in Seeding");
                }

                _mealmateContext.Entry(user).State = EntityState.Unchanged;
            }
        }

        private async Task SeedRolesAsync()
        {
            var role = await _roleManager.FindByNameAsync("Admin");
            if (role == null)
            {
                role = new Role
                {
                    Name = "Admin"
                };

                var result = await _roleManager.CreateAsync(role);
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create role in Seeding");
                }

                _mealmateContext.Entry(role).State = EntityState.Unchanged;
            }
        }
    }
}
