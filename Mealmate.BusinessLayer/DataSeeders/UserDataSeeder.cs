using Mealmate.DataAccess.Contexts;
using Mealmate.Entities.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.BusinessLayer.DataSeeders
{
    public class UserDataSeeder
    {
        private readonly MealmateDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _hosting;

        public UserDataSeeder(MealmateDbContext context,
            UserManager<User> userManager,
            IWebHostEnvironment hosting)
        {
            _context = context;
            _userManager = userManager;
            _hosting = hosting;
        }

        public async Task Seed()
        {
            string fileName = "user-data";
            await _context.Database.EnsureCreatedAsync();

            if (!_userManager.Users.Any())
            {
                var file = Path.Combine(_hosting.ContentRootPath, $"Data/{fileName}.json");
                var json = File.ReadAllText(file);

                var users = JsonConvert.DeserializeObject<List<User>>(json);

                foreach (var user in users)
                {
                    var result = await _userManager.CreateAsync(
                                            user: user,
                                            password: "password");


                }
            }

        }
    }
}
