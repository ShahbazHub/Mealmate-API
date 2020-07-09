using Mealmate.Core.Entities.Base;

namespace Mealmate.Core.Repositories.Base
{
    public interface IEnumRepository<T> : IRepositoryBase<T, int> where T : IEntityBase<int>
    {
    }
}
