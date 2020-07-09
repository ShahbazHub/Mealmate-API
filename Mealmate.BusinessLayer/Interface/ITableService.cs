// using Mealmate.DataAccess.Entities.Mealmate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mealmate.DataAccess.Entities.Mealmate;

namespace Mealmate.BusinessLayer.Interface
{
    public interface ITableService
    {
        IEnumerable<Table> Get();
        Table GetById(int id);
        int Create(Table model);
        int Update(int id, Table model);
        bool Delete(int id);
    }
}
