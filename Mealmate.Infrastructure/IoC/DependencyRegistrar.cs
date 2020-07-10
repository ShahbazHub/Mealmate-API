using Mealmate.Core.Interfaces;
using Mealmate.Core.Repositories;
using Mealmate.Core.Repositories.Base;
using Mealmate.Infrastructure.Behaviors;
using Mealmate.Infrastructure.Data;
using Mealmate.Infrastructure.Logging;
using Mealmate.Infrastructure.Misc;
using Mealmate.Infrastructure.Repository;
using Mealmate.Infrastructure.Repository.Base;
using Autofac;
using MediatR;
using System.Reflection;

namespace Mealmate.Infrastructure.IoC
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            // repositories
            builder.RegisterType<RestaurantRepository>().As<IRestaurantRepository>().InstancePerRequest();
            
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerRequest();
            builder.RegisterGeneric(typeof(EnumRepository<>)).As(typeof(IEnumRepository<>)).InstancePerRequest();
            builder.RegisterGeneric(typeof(RepositoryBase<,>)).As(typeof(IRepositoryBase<,>)).InstancePerRequest();

            builder.RegisterGeneric(typeof(LoggerAdapter<>)).As(typeof(IAppLogger<>)).InstancePerRequest();

            builder.RegisterType<MealmateContextSeed>();

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                .AsImplementedInterfaces();

            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            //var handlerAssemblies = typeFinder.FindClassesOfType(typeof(IRequestHandler<,>))
            //    .Select(t => t.Assembly).Distinct().ToArray();
            //builder.RegisterAssemblyTypes(handlerAssemblies)
            //    .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder.Register<ServiceFactory>(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();
                return t => { object o; return componentContext.TryResolve(t, out o) ? o : null; };
            });

            builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));

            //// Register the Command's Validators (Validators based on FluentValidation library)
            //var validatorAssemblies = typeFinder.FindClassesOfType(typeof(IValidator<,>))
            //    .Select(t => t.Assembly).Distinct().ToArray();
            //builder.RegisterAssemblyTypes(validatorAssemblies).
            //    Where(t => t.IsClosedTypeOf(typeof(IValidator<>))).AsImplementedInterfaces();
        }

        public int Order => 1;
    }
}
