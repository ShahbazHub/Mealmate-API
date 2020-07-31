using Mealmate.Api.Services;
using Mealmate.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mealmate.Api.Installers
{
    public class GoogleAuthInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var googleAuthSettings = new GoogleAuthSettings();
            configuration.Bind(nameof(GoogleAuthSettings), googleAuthSettings);
            services.AddSingleton(googleAuthSettings);

            services.AddHttpClient();
            services.AddSingleton<IGoogleAuthService, GoogleAuthService>();
        }
    }
}
