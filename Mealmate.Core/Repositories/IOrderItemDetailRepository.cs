using Mealmate.Core.Entities;
using Mealmate.Core.Entities.Lookup;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Core.Repositories
{
    public interface IOrderItemDetailRepository : IRepository<OrderItemDetail>
    {
        Task<IPagedList<OrderItemDetail>> SearchAsync(PageSearchArgs args);
    }
}
