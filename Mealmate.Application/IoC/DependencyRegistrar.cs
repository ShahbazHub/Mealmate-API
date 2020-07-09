using Mealmate.Application.Interfaces;
using Mealmate.Application.Services;
using Mealmate.Infrastructure.IoC;
using Mealmate.Infrastructure.Misc;
using Autofac;

namespace Mealmate.Application.IoC
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            // services
            builder.RegisterType<RestaurantService>().As<IRestaurantService>().InstancePerLifetimeScope();
        }

        public int Order => 2;
    }
}
