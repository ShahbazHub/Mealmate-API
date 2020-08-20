using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities;
using Mealmate.Core.Interfaces;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories;
using Mealmate.Core.Specifications;
using Mealmate.Infrastructure.Data;
using Mealmate.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;

namespace Mealmate.Application.Services
{
    public class BranchService : IBranchService
    {
        private readonly IMenuItemService _menuItemService;
        private readonly MealmateContext _context;
        private readonly IUserBranchRepository _userBranchRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IAppLogger<BranchService> _logger;
        private readonly IMapper _mapper;

        public BranchService(
            IUserBranchRepository userBranchRepository,
            IBranchRepository branchRepository,
            IAppLogger<BranchService> logger,
            IMapper mapper,
            IMenuItemService menuItemService,
            MealmateContext context)
        {
            _menuItemService = menuItemService;
            _context = context;
            _userBranchRepository = userBranchRepository ?? throw new ArgumentNullException(nameof(userBranchRepository));
            _branchRepository = branchRepository ?? throw new ArgumentNullException(nameof(branchRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<BranchModel> Create(BranchCreateModel model)
        {
            var newbranch = new Branch
            {
                Address = model.Address,
                Created = DateTime.Now,
                IsActive = true,
                Name = model.Name,
                RestaurantId = model.RestaurantId,
                Latitude = model.Latitude,
                Longitude = model.Longitude
            };

            newbranch = await _branchRepository.SaveAsync(newbranch);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newbranchmodel = _mapper.Map<BranchModel>(newbranch);
            return newbranchmodel;
        }

        public async Task Delete(int id)
        {
            var existingBranch = await _branchRepository.GetByIdAsync(id);
            if (existingBranch == null)
            {
                throw new ApplicationException("Branch with this id is not exists");
            }

            existingBranch.IsActive = false;

            await _branchRepository.SaveAsync(existingBranch);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IEnumerable<BranchModel>> Get(int restaurantId)
        {
            var result = await _branchRepository.GetAsync(x => x.RestaurantId == restaurantId);
            return _mapper.Map<IEnumerable<BranchModel>>(result);
        }

        public async Task<IEnumerable<BranchModel>> GetByEmployee(int employeeId)
        {
            List<BranchModel> result = new List<BranchModel>();
            var model = await _userBranchRepository.SearchAsync(employeeId);

            var data = _mapper.Map<IEnumerable<UserBranchModel>>(model);
            foreach (var item in data)
            {
                result.Add(item.Branch);
            }
            return result;
        }

        public async Task<BranchModel> GetById(int id)
        {
            BranchModel result = null;

            var model = await _branchRepository.GetByIdAsync(id);
            if (model != null)
            {
                result = _mapper.Map<BranchModel>(model);
            }

            return result;
        }

        public async Task Update(int id, BranchUpdateModel model)
        {
            var existingBranch = await _branchRepository.GetByIdAsync(id);
            if (existingBranch == null)
            {
                throw new ApplicationException("Branch with this id is not exists");
            }

            existingBranch.Address = model.Address;
            existingBranch.Name = model.Name;
            existingBranch.IsActive = model.IsActive;
            existingBranch.Latitude = model.Latitude;
            existingBranch.Longitude = model.Longitude;

            await _branchRepository.SaveAsync(existingBranch);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<BranchModel>> Search(BranchSearchModel model, PageSearchArgs args)
        {
            var temp = await _branchRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var result = _mapper.Map<List<BranchModel>>(temp.Items);

            // Calculations for:
            //                  Total dishes
            //                  Available dishes after applying filter

            foreach (var branch in result)
            {
                var totalDishes = branch
                                    .Menus
                                    .SelectMany(p => p.MenuItems)
                                    .Count(p => p.IsActive == true);

                branch.TotalDishes = totalDishes;
                int filteredMenus = 0;

                var menuItems = branch.Menus.SelectMany(mi => mi.MenuItems);
                if (model.CuisineTypes.Count > 0)
                {
                    menuItems = menuItems.Where(p => model.CuisineTypes.Contains(p.CuisineTypeId));
                }

                filteredMenus = menuItems.Count();

                if (model.Allergens.Count > 0 || model.Dietaries.Count > 0)
                {

                    foreach (var item in menuItems)
                    {
                        if (model.Allergens.Count > 0 && model.Dietaries.Count > 0)
                        {
                            var status = false;
                            var detailAllergens = await _menuItemService.GetAllergens(item.Id);
                            if (detailAllergens != null)
                            {
                                foreach (var detailAllergenId in detailAllergens)
                                {
                                    foreach (var allergenId in model.Allergens)
                                    {
                                        if (allergenId == detailAllergenId)
                                        {
                                            status = true;
                                            break;
                                        }
                                    }
                                    if (status == true)
                                    {
                                        break;
                                    }
                                }

                                if (status == true)
                                {
                                    filteredMenus -= 1;
                                }
                                else
                                {
                                    var detailDietaries = await _menuItemService.GetDietaries(item.Id);
                                    if (detailDietaries != null)
                                    {
                                        var dietaryStatus = true;
                                        foreach (var dietaryId in model.Dietaries)
                                        {
                                            if (!detailDietaries.Any(p => p == dietaryId))
                                            {
                                                dietaryStatus = false;
                                                break;
                                            }
                                        }
                                        if (dietaryStatus == false)
                                        {
                                            filteredMenus -= 1;
                                        }
                                    }

                                }
                            }
                        }
                        else if (model.Allergens.Count == 0 && model.Dietaries.Count > 0)
                        {
                            var detailDietaries = await _menuItemService.GetDietaries(item.Id);
                            if (detailDietaries != null)
                            {
                                var dietaryStatus = true;
                                foreach (var dietaryId in model.Dietaries)
                                {
                                    if (!detailDietaries.Any(p => p == dietaryId))
                                    {
                                        dietaryStatus = false;
                                        break;
                                    }
                                }
                                if (dietaryStatus == false)
                                {
                                    filteredMenus -= 1;
                                }
                            }
                        }
                        else if (model.Allergens.Count > 0 && model.Dietaries.Count == 0)
                        {
                            var status = false;
                            var detailAllergens = await _menuItemService.GetAllergens(item.Id);
                            if (detailAllergens != null)
                            {
                                foreach (var detailAllergenId in detailAllergens)
                                {
                                    foreach (var allergenId in model.Allergens)
                                    {
                                        if (allergenId == detailAllergenId)
                                        {
                                            status = true;
                                            break;
                                        }
                                    }
                                    if (status == true)
                                    {
                                        break;
                                    }
                                }

                                if (status == true)
                                {
                                    filteredMenus -= 1;
                                }
                            }
                        }


                    }
                }
                branch.FilteredDishes = filteredMenus;
            }

            var pagedList = new PagedList<BranchModel>(
                temp.PageIndex,
                temp.PageSize,
                temp.TotalCount,
                temp.TotalPages,
                result);

            return pagedList;
        }

        public async Task<IPagedList<BranchModel>> Search(int restaurantId, int isActive, PageSearchArgs args)
        {
            var TablePagedList = await _branchRepository.SearchAsync(restaurantId, isActive, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<BranchModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<BranchModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }

        public async Task<BranchInfoModel> GetBranchInfoById(int id)
        {
            var branchInfo = await _branchRepository
                            .Table
                            .Include(b => b.Restaurant)
                            .Where(b=> b.Id ==id)
                            .Select(x => new BranchInfoModel
                            {
                                Address = x.Address,
                                ContactNumber =  x.ContactNumber,
                                ServiceTimeFrom =  x.ServiceTimeFrom,
                                ServiceTimeTo =  x.ServiceTimeTo,
                                Description = x.Restaurant.Description
                            }).FirstOrDefaultAsync();

            return branchInfo;
        }
    }
}
