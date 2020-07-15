using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemModel>> Get(int orderId);
        Task<OrderItemModel> GetById(int id);
        Task<OrderItemModel> Create(OrderItemModel model);
        Task Update(OrderItemModel model);
        Task Delete(int id);
        Task<IPagedList<OrderItemModel>> Search(PageSearchArgs args);

    }
}
