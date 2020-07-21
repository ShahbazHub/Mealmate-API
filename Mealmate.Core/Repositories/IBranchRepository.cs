using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Core.Repositories
{
    public interface IBranchRepository : IRepository<Branch>
    {
        Task<IPagedList<Branch>> SearchAsync(PageSearchArgs args);
        Task<IPagedList<Branch>> SearchAsync(int restaurantId, int isActive, PageSearchArgs args);
    }
}
