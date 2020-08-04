using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IRestroomRequestStateService
    {
        Task<IEnumerable<RestroomRequestStateModel>> Get();
        Task<RestroomRequestStateModel> GetById(int id);
        Task<RestroomRequestStateModel> Create(RestroomRequestStateCreateModel model);
        Task Update(int id, RestroomRequestStateUpdateModel model);
        Task Delete(int id);
        Task<IPagedList<RestroomRequestStateModel>> Search(int isActive, PageSearchArgs args);
    }
}
