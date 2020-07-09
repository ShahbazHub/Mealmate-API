// using Mealmate.DataAccess.Entities.Mealmate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mealmate.DataAccess.Entities.Mealmate;

namespace Mealmate.BusinessLayer.Interface
{
    public interface IBranchService
    {
        IEnumerable<Branch> Get();
        Branch GetById(int id);
        int Create(Branch model);
        int Update(int id, Branch model);
        bool Delete(int id);
    }
}
