using System;

using Mealmate.BusinessLayer.DataSeeders;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mealmate.Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Run data seeding
            RunSeeding(host);

            // Running web host
            host.Run();

        }
        private static void RunSeeding(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var appContextUser = scope.ServiceProvider.GetRequiredService<UserDataSeeder>();
            try
            {
                appContextUser.Seed().Wait();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while seeding users' data", ex);
            }

            var appContextRole = scope.ServiceProvider.GetRequiredService<RoleDataSeeder>();
            try
            {
                appContextRole.Seed().Wait();
            }
            catch (Exception ex)
            {
                throw new Exception("Error while seeding roles' data", ex);
            }
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
    }
}
