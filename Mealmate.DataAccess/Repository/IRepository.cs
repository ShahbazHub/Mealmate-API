using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.DataAccess.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> Get();
        TEntity GetFirstOrDefault(Func<TEntity, bool> predicate);
        TEntity GetSingle(Func<TEntity, bool> predicate);

        IEnumerable<TEntity> Find(Func<TEntity, bool> where);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
