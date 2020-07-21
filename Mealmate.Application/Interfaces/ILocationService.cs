using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationModel>> Get(int branchId);
        Task<LocationModel> GetById(int id);
        Task<LocationModel> Create(LocationCreateModel model);
        Task Update(int id, LocationUpdateModel model);
        Task Delete(int id);
        Task<IPagedList<LocationModel>> Search(int branchId, int isActive, PageSearchArgs args);
    }
}
