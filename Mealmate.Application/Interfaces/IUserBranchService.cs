using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Application.Interfaces
{
    public interface IUserBranchService
    {
        Task<IEnumerable<UserBranchModel>> Get(int UserId);
        Task<UserBranchModel> GetById(int id);
        Task<UserBranchModel> Create(UserBranchCreateModel model);
        Task Update(int id, UserBranchUpdateModel model);
        Task Delete(int id);

        Task<IPagedList<UserBranchModel>> Search(int employeeId, PageSearchArgs args);
        Task<IPagedList<UserModel>> List(int branchId, PageSearchArgs args);
    }
}
