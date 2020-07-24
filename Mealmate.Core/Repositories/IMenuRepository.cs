using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Core.Repositories
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<IPagedList<Menu>> SearchAsync(PageSearchArgs args);
        Task<IPagedList<Menu>> SearchAsync(int branchId, int isActive, PageSearchArgs args);
    }
}
