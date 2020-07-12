using Mealmate.Core.Entities;
using Mealmate.Core.Specifications.Base;

using System;
using System.Linq.Expressions;

namespace Mealmate.Core.Specifications
{
    public class RestaurantWithBranchesSpecification : BaseSpecification<Restaurant>
    {
        public RestaurantWithBranchesSpecification(string restaurantName)
            : base(p => p.Name.Contains(restaurantName))
        {
            AddInclude(p => p.Branches);
        }

        public RestaurantWithBranchesSpecification(int restaurantId)
            : base(p => p.Id == restaurantId)
        {
            AddInclude(p => p.Branches);
        }

        public RestaurantWithBranchesSpecification(Expression<Func<Restaurant, bool>> predicate)
            : base(predicate)
        {
            AddInclude(p => p.Branches);
        }
        public RestaurantWithBranchesSpecification()
           : base(null)
        {
            AddInclude(p => p.Branches);
        }
    }
}
