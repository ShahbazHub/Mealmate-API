using Mealmate.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.BusinessLayer.Interface
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchModel>> Get(int restaurantId);
        Task<BranchModel> GetById(int id);
        Task<BranchModel> Create(BranchModel model);
        Task Update(BranchModel model);
        Task Delete(int id);
    }
}
