using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IOptionItemDietaryService
    {
        Task<IEnumerable<OptionItemDietaryModel>> Get(int optionItemId);
        Task<OptionItemDietaryModel> GetById(int id);
        Task<OptionItemDietaryModel> Create(OptionItemDietaryCreateModel model);
        Task Update(int id, OptionItemDietaryUpdateModel model);
        Task Delete(int id);

        Task<IPagedList<OptionItemDietaryModel>> Search(PageSearchArgs args);
        Task<IPagedList<OptionItemDietaryModel>> Search(int optionItemId, int isActive, PageSearchArgs args);
    }
}
