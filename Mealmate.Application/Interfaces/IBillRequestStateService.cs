using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IBillRequestStateService
    {
        Task<IEnumerable<BillRequestStateModel>> Get();
        Task<BillRequestStateModel> GetById(int id);
        Task<BillRequestStateModel> Create(BillRequestStateCreateModel model);
        Task Update(int id, BillRequestStateUpdateModel model);
        Task Delete(int id);
        Task<IPagedList<BillRequestStateModel>> Search(int isActive, PageSearchArgs args);
    }
}
