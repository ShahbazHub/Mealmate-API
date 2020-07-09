// using Mealmate.DataAccess.Entities.Mealmate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mealmate.DataAccess.Entities.Mealmate;

namespace Mealmate.BusinessLayer.Interface
{
    public interface IMenuItemService
    {
        IEnumerable<MenuItem> Get();
        MenuItem GetById(int id);
        int Create(MenuItem model);
        int Update(int id, MenuItem model);
        bool Delete(int id);
    }
}
