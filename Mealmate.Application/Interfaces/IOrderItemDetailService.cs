using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IOrderItemDetailService
    {
        Task<IEnumerable<OrderItemDetailModel>> Get(int restaurantId);
        Task<OrderItemDetailModel> GetById(int id);
        Task<OrderItemDetailModel> Create(OrderItemDetailModel model);
        Task Update(OrderItemDetailModel model);
        Task Delete(int id);
        Task<IPagedList<OrderItemDetailModel>> Search(PageSearchArgs args);

    }
}
