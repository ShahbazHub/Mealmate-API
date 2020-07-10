using Mealmate.Application.Interfaces;
using Mealmate.Application.Services;
using Mealmate.Infrastructure.IoC;
using Mealmate.Infrastructure.Misc;
using Autofac;
using Autofac.Core;
using AutoMapper;

namespace Mealmate.Application.IoC
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {

            // services

            builder.RegisterType<RestaurantService>().As<IRestaurantService>().InstancePerLifetimeScope();
            builder.RegisterType<BranchService>().As<IBranchService>().InstancePerLifetimeScope();
            builder.RegisterType<LocationService>().As<ILocationService>().InstancePerLifetimeScope();
            builder.RegisterType<MenuItemOptionService>().As<IMenuItemOptionService>().InstancePerLifetimeScope();
            builder.RegisterType<MenuItemService>().As<IMenuItemService>().InstancePerLifetimeScope();
            builder.RegisterType<MenuService>().As<IMenuService>().InstancePerLifetimeScope();
            builder.RegisterType<OptionItemService>().As<IOptionItemService>().InstancePerLifetimeScope();
            builder.RegisterType<QRCodeService>().As<IQRCodeService>().InstancePerLifetimeScope();
            builder.RegisterType<LocationService>().As<ILocationService>().InstancePerLifetimeScope();
         
            }
        public int Order => 2;
    }
}
