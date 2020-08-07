using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderModel>> Get(int customerId);
        Task<IEnumerable<OrderModel>> Get(int restaurantId, int orderStateId);
        Task<OrderModel> GetById(int id);
        Task<OrderModel> Create(OrderCreateModel model);
        Task Update(int id, OrderUpdateModel model);
        Task Delete(int id);
        Task<IPagedList<OrderModel>> Search(PageSearchArgs args);

    }
}
