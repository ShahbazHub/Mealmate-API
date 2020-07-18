using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Core.Repositories
{
    public interface IOptionItemAllergenRepository : IRepository<OptionItemAllergen>
    {
        Task<IPagedList<OptionItemAllergen>> SearchAsync(PageSearchArgs args);
        Task<IPagedList<OptionItemAllergen>> SearchAsync(int optionItemId, PageSearchArgs args);
    }
}
