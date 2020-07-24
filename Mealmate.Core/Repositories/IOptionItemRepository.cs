using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Core.Repositories
{
    public interface IOptionItemRepository : IRepository<OptionItem>
    {
        Task<IPagedList<OptionItem>> SearchAsync(PageSearchArgs args);
        Task<IPagedList<OptionItem>> SearchAsync(int branchId, int isActive, PageSearchArgs args);
    }
}
