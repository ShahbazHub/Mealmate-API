// using Mealmate.DataAccess.Entities.Mealmate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mealmate.DataAccess.Entities.Mealmate;

namespace Mealmate.BusinessLayer.Interface
{
    public interface IMenuService
    {
        IEnumerable<Menu> Get();
        Menu GetById(int id);
        int Create(Menu model);
        int Update(int id, Menu model);
        bool Delete(int id);
    }
}
