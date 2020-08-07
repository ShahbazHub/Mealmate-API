using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mealmate.Core.Repositories
{
    public interface IUserBranchRepository : IRepository<UserBranch>
    {
        Task<IEnumerable<UserBranch>> SearchAsync(int employeeId);
        Task<IPagedList<UserBranch>> SearchAsync(int employeeId, PageSearchArgs args);
        Task<IPagedList<UserBranch>> ListAsync(int branchId, PageSearchArgs args);
    }
}
