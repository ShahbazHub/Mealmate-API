using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderModel>> Get(int customerId);
        Task<OrderModel> GetById(int id);
        Task<OrderModel> Create(OrderModel model);
        Task Update(OrderModel model);
        Task Delete(int id);
        Task<IPagedList<OrderModel>> Search(PageSearchArgs args);

    }
}
