using Mealmate.DataAccess.Contexts;
using Mealmate.Entities;
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
    public class RoleDataSeeder
    {
        private readonly MealmateDbContext _context;
        private readonly RoleManager<Role> _roleManager;
        private readonly IWebHostEnvironment _hosting;

        public RoleDataSeeder(MealmateDbContext context,
            RoleManager<Role> roleManager,
            IWebHostEnvironment hosting)
        {
            _context = context;
            _roleManager = roleManager;
            _hosting = hosting;
        }

        public async Task Seed()
        {
            string fileName = "role-data";
            await _context.Database.EnsureCreatedAsync();

            if (!_roleManager.Roles.Any())
            {
                var file = Path.Combine(_hosting.ContentRootPath, $"Data/{fileName}.json"); 
                var json = File.ReadAllText(file);

                var roles = JsonConvert.DeserializeObject<List<Role>>(json);

                foreach (var role in roles)
                {
                    var result = await _roleManager.CreateAsync(role);
                }
            }
        }
    }
}
