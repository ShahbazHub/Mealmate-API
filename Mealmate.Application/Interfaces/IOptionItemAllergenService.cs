using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IOptionItemAllergenService
    {
        Task<IEnumerable<OptionItemAllergenModel>> Get(int optionItemId);
        Task<OptionItemAllergenModel> GetById(int id);
        Task<OptionItemAllergenModel> Create(OptionItemAllergenModel model);
        Task Update(OptionItemAllergenModel model);
        Task Delete(int id);

        Task<IPagedList<OptionItemAllergenModel>> Search(PageSearchArgs args);
        Task<IPagedList<OptionItemAllergenModel>> Search(int optionItemId, PageSearchArgs args);
    }
}
