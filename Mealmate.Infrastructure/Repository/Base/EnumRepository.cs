using Mealmate.Core.Entities.Base;
using Mealmate.Core.Repositories.Base;
using Mealmate.Infrastructure.Data;

namespace Mealmate.Infrastructure.Repository.Base
{
    public class EnumRepository<T> : RepositoryBase<T, int>, IEnumRepository<T>
        where T : class, IEntityBase<int>
    {
        public EnumRepository(MealmateContext context)
            : base(context)
        {
        }
    }
}
