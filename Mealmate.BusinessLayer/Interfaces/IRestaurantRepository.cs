using Mealmate.DataAccess.Repository;
using Mealmate.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.BusinessLayer.Interfaces
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
    }
}
