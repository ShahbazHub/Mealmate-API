using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Core.Repositories
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<IPagedList<Location>> SearchAsync(PageSearchArgs args);
        Task<IPagedList<Location>> SearchAsync(int branchId, PageSearchArgs args);
    }
}
