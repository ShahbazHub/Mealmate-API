// using Mealmate.DataAccess.Entities.Mealmate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mealmate.DataAccess.Entities.Mealmate;

namespace Mealmate.BusinessLayer.Interface
{
    public interface ILocationService
    {
        IEnumerable<Location> Get();
        Location GetById(int id);
        int Create(Location model);
        int Update(int id, Location model);
        bool Delete(int id);
    }
}
