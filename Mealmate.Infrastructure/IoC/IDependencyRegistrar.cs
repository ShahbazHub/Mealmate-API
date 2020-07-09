using Mealmate.Infrastructure.Misc;
using Autofac;

namespace Mealmate.Infrastructure.IoC
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        int Order { get; }
    }
}
