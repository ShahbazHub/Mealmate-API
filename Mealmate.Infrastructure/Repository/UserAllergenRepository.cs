using Mealmate.Core.Entities;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories;
using Mealmate.Core.Specifications;
using Mealmate.Infrastructure.Data;
using Mealmate.Infrastructure.Paging;
using Mealmate.Infrastructure.Repository.Base;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mealmate.Infrastructure.Repository
{
    public class UserAllergenRepository : Repository<UserAllergen>, IUserAllergenRepository
    {
        public UserAllergenRepository(MealmateContext context)
            : base(context)
        {
        }

        public Task<IPagedList<UserAllergen>> SearchAsync(PageSearchArgs args)
        {
            var query = Table.Include(p => p.Allergen);

            var orderByList = new List<Tuple<SortingOption, Expression<Func<UserAllergen, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<UserAllergen, object>>>(sortingOption, p => p.Id));
                            break;
                        case "name":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<UserAllergen, object>>>(sortingOption, p => p.Allergen.Name));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<UserAllergen, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, p => p.Id));
            }

            //TODO: FilteringOption.Operator will be handled
            var filterList = new List<Tuple<FilteringOption, Expression<Func<UserAllergen, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<UserAllergen, bool>>>(filteringOption, p => p.Id == (int)filteringOption.Value));
                            break;
                        case "name":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<UserAllergen, bool>>>(filteringOption, p => p.Allergen.Name.Contains((string)filteringOption.Value)));
                            break;
                    }
                }
            }

            var pagedList = new PagedList<UserAllergen>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<UserAllergen>>(pagedList);
        }

        public Task<IPagedList<UserAllergen>> SearchAsync(int userId, int isActive, PageSearchArgs args)
        {
            var query = Table.Include(p => p.Allergen).Where(p => p.UserId == userId);
            if (isActive == 1 || isActive == 0)
            {
                var status = isActive == 1 ? true : false;
                query = query.Where(p => p.IsActive == status);
            }
            var orderByList = new List<Tuple<SortingOption, Expression<Func<UserAllergen, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<UserAllergen, object>>>(sortingOption, p => p.Id));
                            break;
                        case "name":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<UserAllergen, object>>>(sortingOption, p => p.Allergen.Name));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<UserAllergen, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, p => p.Id));
            }

            //TODO: FilteringOption.Operator will be handled
            var filterList = new List<Tuple<FilteringOption, Expression<Func<UserAllergen, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<UserAllergen, bool>>>(filteringOption, p => p.Id == (int)filteringOption.Value));
                            break;
                        case "name":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<UserAllergen, bool>>>(filteringOption, p => p.Allergen.Name.Contains((string)filteringOption.Value)));
                            break;
                    }
                }
            }

            var pagedList = new PagedList<UserAllergen>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<UserAllergen>>(pagedList);
        }
    }
}
