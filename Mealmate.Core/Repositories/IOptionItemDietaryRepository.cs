using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Core.Repositories
{
    public interface IOptionItemDietaryRepository : IRepository<OptionItemDietary>
    {
        Task<IPagedList<OptionItemDietary>> SearchAsync(PageSearchArgs args);
        Task<IPagedList<OptionItemDietary>> SearchAsync(int optionItemId, PageSearchArgs args);
    }
}
