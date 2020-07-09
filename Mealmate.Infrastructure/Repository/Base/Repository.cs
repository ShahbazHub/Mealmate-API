using Mealmate.Core.Entities.Base;
using Mealmate.Core.Repositories.Base;
using Mealmate.Infrastructure.Data;

namespace Mealmate.Infrastructure.Repository.Base
{
    public class Repository<T> : RepositoryBase<T, int>, IRepository<T>
        where T : class, IEntityBase<int>
    {
        public Repository(MealmateContext context)
            : base(context)
        {
        }
    }
}
