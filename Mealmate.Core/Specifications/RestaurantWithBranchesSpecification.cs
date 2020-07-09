using Mealmate.Core.Entities;
using Mealmate.Core.Specifications.Base;

namespace Mealmate.Core.Specifications
{
    public class RestaurantWithBranchesSpecification : BaseSpecification<Restaurant>
    {
        public RestaurantWithBranchesSpecification(string productName)
            : base(p => p.Name.Contains(productName))
        {
            AddInclude(p => p.Branches);
        }

        public RestaurantWithBranchesSpecification(int productId)
            : base(p => p.Id == productId)
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
