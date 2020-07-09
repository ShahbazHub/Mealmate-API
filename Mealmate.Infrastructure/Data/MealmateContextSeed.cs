using Mealmate.Core.Entities;
using Mealmate.Core.Repositories;
using Mealmate.Core.Repositories.Base;
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
        private readonly IRestaurantRepository _restaurantRepository;
        //private readonly IRepository<Address> _addressRepository;

        public MealmateContextSeed(
            MealmateContext aspnetRunContext,
            UserManager<User> userManager,
            IRestaurantRepository restaurantRepository
            //IRepository<Address> addressRepository
            )
        {
            _mealmateContext = aspnetRunContext;
            _userManager = userManager;
            _restaurantRepository = restaurantRepository;
            //_addressRepository = addressRepository;
        }

        public async Task SeedAsync()
        {
            // TODO: Only run this if using a real database
            // _aspnetRunContext.Database.Migrate();
            // _aspnetRunContext.Database.EnsureCreated();


            // users
            await SeedUsersAsync();
        }


        

        private async Task SeedUsersAsync()
        {
            var user = await _userManager.FindByEmailAsync("aspnetrun@outlook.com");
            if (user == null)
            {
                user = new User
                {
                    Name = "Waseem Ahmad",
                    Email = "waseem.ahmad.mughal@gmail.com",
                    UserName = "wamughal"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create user in Seeding");
                }

                _mealmateContext.Entry(user).State = EntityState.Unchanged;
            }
        }
    }
}
