using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IOptionItemService
    {
        Task<IEnumerable<OptionItemModel>> Get();
        Task<OptionItemModel> GetById(int id);
        Task<OptionItemModel> Create(OptionItemCreateModel model);
        Task<OptionItemModel> Create(OptionItemDetailCreateModel model);
        Task Update(int id, OptionItemUpdateModel model);
        Task Update(int id, OptionItemDetailUpdateModel model);
        Task Delete(int id);

        Task<IPagedList<OptionItemModel>> Search(PageSearchArgs args);
        Task<IPagedList<OptionItemModel>> Search(int branchId, int isActive, PageSearchArgs args);
    }
}
