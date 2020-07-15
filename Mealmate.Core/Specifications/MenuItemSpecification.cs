using Mealmate.Core.Entities;
using Mealmate.Core.Specifications.Base;

using System;
using System.Linq.Expressions;

namespace Mealmate.Core.Specifications
{
    public class MenuItemSpecification : BaseSpecification<MenuItem>
    {
        public MenuItemSpecification(Expression<Func<MenuItem, bool>> predicate)
            : base(predicate)
        {
            AddInclude(p => p.MenuItemAllergens);
            AddInclude(p => p.MenuItemDietaries);
        }

        public MenuItemSpecification()
           : base(null)
        {
            AddInclude(p => p.MenuItemAllergens);
            AddInclude(p => p.MenuItemDietaries);
        }
    }
}
