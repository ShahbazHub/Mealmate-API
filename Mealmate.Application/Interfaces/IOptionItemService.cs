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
        Task<OptionItemModel> Create(OptionItemModel model);
        Task Update(OptionItemModel model);
        Task Delete(int id);

        Task<IPagedList<OptionItemModel>> Search(PageSearchArgs args);
        Task<IPagedList<OptionItemModel>> Search(int branchId, PageSearchArgs args);
    }
}
