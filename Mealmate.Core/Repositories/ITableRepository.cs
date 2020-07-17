using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Core.Repositories
{
    public interface ITableRepository : IRepository<Table>
    {
        Task<IPagedList<Table>> SearchAsync(PageSearchArgs args);
        Task<IPagedList<Table>> SearchAsync(int locationId, PageSearchArgs args);
    }
}
