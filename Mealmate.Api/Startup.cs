using System;
using System.IO;

using FirebaseAdmin;

using Google.Apis.Auth.OAuth2;

using Mealmate.Api.Application.Middlewares;
using Mealmate.Core.Configuration;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Mealmate.Api
{
    public class Startup
    {
        public IConfigurationRoot _config { get; }
        public IWebHostEnvironment _env { get; }
        public MealmateSettings _mealMateSettings { get; }

        public Startup(IWebHostEnvironment env)
        {
            _config = new ConfigurationBuilder()
                       .SetBasePath(env.ContentRootPath)
                       .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                       .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                       .AddEnvironmentVariables()
                       .Build();

            _env = env;
            _mealMateSettings = _config.Get<MealmateSettings>();

            var pathToKey = Path.Combine(Directory.GetCurrentDirectory(), "pushnotificationsvc.json");
            FirebaseApp.Create(new AppOptions
            {
                Credential = GoogleCredential.FromFile(pathToKey)
            });
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            return services
                .AddCustomMvc()
                .AddCustomDbContext(_mealMateSettings)
                .AddCustomIdentity()
                .AddCustomSwagger()
                .AddCustomConfiguration(_config)
                .AddCustomAuthentication(_mealMateSettings)
                .AddCustomIntegrations(_env);

            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            //else
            //{
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            // Enable middleware to serve generated Swagger as a JSON endpoint.

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Angular");
                options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });
            app.UseMiddleware<LoggingMiddleware>();
            //app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
