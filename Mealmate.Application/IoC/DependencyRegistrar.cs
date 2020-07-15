using Mealmate.Application.Interfaces;
using Mealmate.Application.Services;
using Mealmate.Infrastructure.IoC;
using Mealmate.Infrastructure.Misc;
using Autofac;
using AutoMapper;
using System.Collections.Generic;
using Mealmate.Application.Mapper;

namespace Mealmate.Application.IoC
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.Register(context => new MapperConfiguration(configuration =>
            {
                foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                {
                    configuration.AddProfile(profile);
                }
            }))
              .AsSelf()
              .InstancePerRequest();

            builder.Register(context => context.Resolve<MapperConfiguration>()
                .CreateMapper(context.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();

            // services

            builder.RegisterType<UserAllergenService>().As<IUserAllergenService>().InstancePerLifetimeScope();
            builder.RegisterType<UserDietaryService>().As<IUserDietaryService>().InstancePerLifetimeScope();

            builder.RegisterType<RestaurantService>().As<IRestaurantService>().InstancePerLifetimeScope();
            builder.RegisterType<BranchService>().As<IBranchService>().InstancePerLifetimeScope();
            builder.RegisterType<LocationService>().As<ILocationService>().InstancePerLifetimeScope();
            builder.RegisterType<MenuItemOptionService>().As<IMenuItemOptionService>().InstancePerLifetimeScope();
            builder.RegisterType<MenuItemService>().As<IMenuItemService>().InstancePerLifetimeScope();
            builder.RegisterType<MenuService>().As<IMenuService>().InstancePerLifetimeScope();
            builder.RegisterType<OptionItemService>().As<IOptionItemService>().InstancePerLifetimeScope();
            builder.RegisterType<QRCodeService>().As<IQRCodeService>().InstancePerLifetimeScope();
            builder.RegisterType<LocationService>().As<ILocationService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<AllergenService>().As<IAllergenService>().InstancePerLifetimeScope();
            builder.RegisterType<DietaryService>().As<IDietaryService>().InstancePerLifetimeScope();
            builder.RegisterType<CuisineTypeService>().As<ICuisineTypeService>().InstancePerLifetimeScope();

            builder.RegisterType<TableService>().As<ITableService>().InstancePerLifetimeScope();


            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderItemService>().As<IOrderItemService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderItemDetailService>().As<IOrderItemDetailService>().InstancePerLifetimeScope();

            builder.RegisterType<MealMateMapper>().As<Profile>();

        }

        public int Order => 2;
    }
}
