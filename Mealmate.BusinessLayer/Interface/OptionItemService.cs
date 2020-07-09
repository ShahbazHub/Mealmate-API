// using Mealmate.DataAccess.Entities.Mealmate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mealmate.DataAccess.Entities.Lookup;
using Mealmate.DataAccess.Entities.Mealmate;

namespace Mealmate.BusinessLayer.Interface
{
    public interface IOptionItemService
    {
        IEnumerable<OptionItem> Get();
        OptionItem GetById(int id);
        int Create(OptionItem model);
        int Update(int id, OptionItem model);
        bool Delete(int id);
    }
}
