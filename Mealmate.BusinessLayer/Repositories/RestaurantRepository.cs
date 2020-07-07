using Mealmate.BusinessLayer.Interfaces;
using Mealmate.DataAccess.Contexts;
using Mealmate.DataAccess.Repository;
using Mealmate.Entities.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.BusinessLayer.Repositories
{
    public class RestaurantRepository : Repository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository(MealmateDbContext context)
            : base(context)
        {
        }

        public MealmateDbContext MealmateContext
        {
            get
            {
                return _context as MealmateDbContext;
            }
        }
    }
}
