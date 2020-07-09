using Mealmate.Api.Application.Commands;
using Mealmate.Api.Application.Validations;
using Mealmate.Infrastructure.IoC;
using Mealmate.Infrastructure.Misc;
using Autofac;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace Mealmate.Api.IoC
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            // Register all the Command classes (they implement IRequestHandler) in assembly holding the Commands
            builder.RegisterAssemblyTypes(typeof(CreateRestaurantCommandHandler).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            // Register the Command's Validators (Validators based on FluentValidation library)
            builder.RegisterAssemblyTypes(typeof(CreateRestaurantRequestValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();
        }

        public int Order => 0;
    }
}
