using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IOrderStateService
    {
        Task<IEnumerable<OrderStateModel>> Get();
        Task<OrderStateModel> GetById(int id);
        Task<OrderStateModel> Create(OrderStateCreateModel model);
        Task Update(int id, OrderStateUpdateModel model);
        Task Delete(int id);
        Task<IPagedList<OrderStateModel>> Search(int isActive, PageSearchArgs args);
    }
}
