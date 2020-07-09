using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mealmate.DataAccess.Entities.Mealmate;

namespace Mealmate.BusinessLayer.Interface
{
    public interface IRestaurantService
    {
        IEnumerable<Restaurant> Get();
        Restaurant GetById(int id);
        int Create(Restaurant model);
        int Update(int id, Restaurant model);
        bool Delete(int id);
    }
}
