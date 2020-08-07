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
    public class UserBranchRepository : Repository<UserBranch>, IUserBranchRepository
    {
        public UserBranchRepository(MealmateContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<UserBranch>> SearchAsync(int employeeId)
        {
            var query = Table.Include(p => p.Branch)
                             .Where(p => p.UserId == employeeId);

            return await query.ToListAsync();
        }

        public Task<IPagedList<UserBranch>> SearchAsync(int employeeId, PageSearchArgs args)
        {
            var query = Table.Include(p => p.Branch).Where(p => p.UserId == employeeId);

            var orderByList = new List<Tuple<SortingOption, Expression<Func<UserBranch, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<UserBranch, object>>>(sortingOption, p => p.Id));
                            break;
                        case "name":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<UserBranch, object>>>(sortingOption, p => p.Branch.Name));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<UserBranch, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, p => p.Id));
            }

            //TODO: FilteringOption.Operator will be handled
            var filterList = new List<Tuple<FilteringOption, Expression<Func<UserBranch, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<UserBranch, bool>>>(filteringOption, p => p.Id == (int)filteringOption.Value));
                            break;
                        case "name":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<UserBranch, bool>>>(filteringOption, p => p.Branch.Name.Contains((string)filteringOption.Value)));
                            break;
                    }
                }
            }

            var pagedList = new PagedList<UserBranch>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<UserBranch>>(pagedList);
        }


        public Task<IPagedList<UserBranch>> ListAsync(int branchId, PageSearchArgs args)
        {
            var query = Table.Include(p => p.Branch)
                             .Include(p => p.User)
                              .Where(p => p.BranchId == branchId);

            var orderByList = new List<Tuple<SortingOption, Expression<Func<UserBranch, object>>>>();

            if (args.SortingOptions != null)
            {
                foreach (var sortingOption in args.SortingOptions)
                {
                    switch (sortingOption.Field)
                    {
                        case "id":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<UserBranch, object>>>(sortingOption, p => p.Id));
                            break;
                        case "name":
                            orderByList.Add(new Tuple<SortingOption, Expression<Func<UserBranch, object>>>(sortingOption, p => p.User.FirstName));
                            break;
                    }
                }
            }

            if (orderByList.Count == 0)
            {
                orderByList.Add(new Tuple<SortingOption, Expression<Func<UserBranch, object>>>(new SortingOption { Direction = SortingOption.SortingDirection.ASC }, p => p.Id));
            }

            //TODO: FilteringOption.Operator will be handled
            var filterList = new List<Tuple<FilteringOption, Expression<Func<UserBranch, bool>>>>();

            if (args.FilteringOptions != null)
            {
                foreach (var filteringOption in args.FilteringOptions)
                {
                    switch (filteringOption.Field)
                    {
                        case "id":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<UserBranch, bool>>>(filteringOption, p => p.Id == (int)filteringOption.Value));
                            break;
                        case "name":
                            filterList.Add(new Tuple<FilteringOption, Expression<Func<UserBranch, bool>>>(filteringOption, p => p.User.FirstName.Contains((string)filteringOption.Value)));
                            break;
                    }
                }
            }

            var pagedList = new PagedList<UserBranch>(query, new PagingArgs { PageIndex = args.PageIndex, PageSize = args.PageSize, PagingStrategy = args.PagingStrategy }, orderByList, filterList);

            return Task.FromResult<IPagedList<UserBranch>>(pagedList);
        }
    }
}
