using Mealmate.Core.Interfaces;
using Mealmate.Core.Repositories;
using Mealmate.Core.Repositories.Base;
//using Mealmate.Infrastructure.Behaviors;
using Mealmate.Infrastructure.Data;
using Mealmate.Infrastructure.Logging;
using Mealmate.Infrastructure.Misc;
using Mealmate.Infrastructure.Repository;
using Mealmate.Infrastructure.Repository.Base;
using Autofac;
//using MediatR;
using System.Reflection;

namespace Mealmate.Infrastructure.IoC
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {

            // repositories
            builder.RegisterType<RestaurantRepository>().As<IRestaurantRepository>().InstancePerDependency();
            builder.RegisterType<BranchRepository>().As<IBranchRepository>().InstancePerDependency();
            builder.RegisterType<LocationRepository>().As<ILocationRepository>().InstancePerDependency();
            builder.RegisterType<UserRestaurantRepository>().As<IUserRestaurantRepository>().InstancePerDependency();
            builder.RegisterType<UserBranchRepository>().As<IUserBranchRepository>().InstancePerDependency();

            builder.RegisterType<MenuRepository>().As<IMenuRepository>().InstancePerDependency();
            builder.RegisterType<MenuItemRepository>().As<IMenuItemRepository>().InstancePerDependency();
            builder.RegisterType<MenuItemOptionRepository>().As<IMenuItemOptionRepository>().InstancePerDependency();

            builder.RegisterType<QRCodeRepository>().As<IQRCodeRepository>().InstancePerDependency();
            builder.RegisterType<TableRepository>().As<ITableRepository>().InstancePerDependency();

            // Lookups
            builder.RegisterType<BillRequestStateRepository>().As<IBillRequestStateRepository>().InstancePerDependency();
            builder.RegisterType<RestroomRequestStateRepository>().As<IRestroomRequestStateRepository>().InstancePerDependency();
            builder.RegisterType<ContactRequestStateRepository>().As<IContactRequestStateRepository>().InstancePerDependency();
            builder.RegisterType<AllergenRepository>().As<IAllergenRepository>().InstancePerDependency();
            builder.RegisterType<DietaryRepository>().As<IDietaryRepository>().InstancePerDependency();
            builder.RegisterType<CuisineTypeRepository>().As<ICuisineTypeRepository>().InstancePerDependency();

            builder.RegisterType<OptionItemRepository>().As<IOptionItemRepository>().InstancePerDependency();
            builder.RegisterType<OptionItemAllergenRepository>().As<IOptionItemAllergenRepository>().InstancePerDependency();
            builder.RegisterType<OptionItemDietaryRepository>().As<IOptionItemDietaryRepository>().InstancePerDependency();

            builder.RegisterType<MenuItemAllergenRepository>().As<IMenuItemAllergenRepository>().InstancePerDependency();
            builder.RegisterType<MenuItemDietaryRepository>().As<IMenuItemDietaryRepository>().InstancePerDependency();


            builder.RegisterType<UserAllergenRepository>().As<IUserAllergenRepository>().InstancePerDependency();
            builder.RegisterType<UserDietaryRepository>().As<IUserDietaryRepository>().InstancePerDependency();

            // Sale
            builder.RegisterType<OrderStateRepository>().As<IOrderStateRepository>().InstancePerDependency();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerDependency();
            builder.RegisterType<OrderItemRepository>().As<IOrderItemRepository>().InstancePerDependency();
            builder.RegisterType<OrderItemDetailRepository>().As<IOrderItemDetailRepository>().InstancePerDependency();

            // Requests
            builder.RegisterType<RestroomRequestRepository>().As<IRestroomRequestRepository>().InstancePerDependency();
            builder.RegisterType<ContactRequestRepository>().As<IContactRequestRepository>().InstancePerDependency();
            builder.RegisterType<BillRequestRepository>().As<IBillRequestRepository>().InstancePerDependency();


            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerDependency();
            builder.RegisterGeneric(typeof(EnumRepository<>)).As(typeof(IEnumRepository<>)).InstancePerDependency();
            builder.RegisterGeneric(typeof(RepositoryBase<,>)).As(typeof(IRepositoryBase<,>)).InstancePerDependency();
            builder.RegisterGeneric(typeof(LoggerAdapter<>)).As(typeof(IAppLogger<>)).InstancePerDependency();

            builder.RegisterType<MealmateContextSeed>().InstancePerDependency();

            //builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
            //    .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            //var handlerAssemblies = typeFinder.FindClassesOfType(typeof(IRequestHandler<,>))
            //    .Select(t => t.Assembly).Distinct().ToArray();
            //builder.RegisterAssemblyTypes(handlerAssemblies)
            //    .AsClosedTypesOf(typeof(IRequestHandler<,>));

            //builder.Register<ServiceFactory>(context =>
            //{
            //    var componentContext = context.Resolve<IComponentContext>();
            //    return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            //});

            //builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
            //// Register the Command's Validators (Validators based on FluentValidation library)
            //var validatorAssemblies = typeFinder.FindClassesOfType(typeof(IValidator<,>))
            //    .Select(t => t.Assembly).Distinct().ToArray();
            //builder.RegisterAssemblyTypes(validatorAssemblies).
            //    Where(t => t.IsClosedTypeOf(typeof(IValidator<>))).AsImplementedInterfaces();
        }

        public int Order => 1;
    }
}
