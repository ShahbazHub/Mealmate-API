// using Mealmate.DataAccess.Entities.Mealmate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mealmate.DataAccess.Entities.Mealmate;

namespace Mealmate.BusinessLayer.Interface
{
    public interface IMenuItemOptionService
    {
        IEnumerable<MenuItemOption> Get();
        MenuItemOption GetById(int id);
        int Create(MenuItemOption model);
        int Update(int id, MenuItemOption model);
        bool Delete(int id);
    }
}
