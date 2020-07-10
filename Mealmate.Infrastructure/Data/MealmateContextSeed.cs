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
        //private readonly IRestaurantRepository _restaurantRepository;
        //private readonly IRepository<Table> _tableRepository;

        public MealmateContextSeed(
            MealmateContext mealmateContext,
            UserManager<User> userManager
            //IRestaurantRepository restaurantRepository,
            //IRepository<Table> tableRepository
            )
        {
            _mealmateContext = mealmateContext;
            _userManager = userManager;
            //_restaurantRepository = restaurantRepository;
            //_tableRepository = tableRepository;
        }

        public async Task SeedAsync()
        {
            //// TODO: Only run this if using a real database
            await _mealmateContext.Database.MigrateAsync();
            await _mealmateContext.Database.EnsureCreatedAsync();

            //// users
            await SeedUsersAsync();
        }
        private async Task SeedUsersAsync()
        {
            var user = await _userManager.FindByEmailAsync("email@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    Name = "Administrator",
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
    }
}
