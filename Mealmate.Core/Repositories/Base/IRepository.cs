using Mealmate.Core.Entities.Base;

namespace Mealmate.Core.Repositories.Base
{
    public interface IRepository<T> : IRepositoryBase<T, int> where T : IEntityBase<int>
    {
    }
}
