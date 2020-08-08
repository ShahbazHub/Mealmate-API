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
            MealmateContext context)
        {
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
                RestaurantId = model.RestaurantId
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

                var menuItems = branch.Menus.SelectMany(mi => mi.MenuItems);
                if (model.CuisineTypes.Count > 0)
                {
                    menuItems = menuItems.Where(p => model.CuisineTypes.Contains(p.CuisineTypeId));
                }

                int filteredMenus = 0;

                foreach (var item in menuItems)
                {
                    if (model.Allergens.Count > 0 && model.Dietaries.Count > 0)
                    {
                        var allergens = _context.MenuItemAllergens
                                                .Include(p => p.Allergen)
                                                .Where(p => p.MenuItemId == item.Id);

                        var resultAllergens = allergens.Where(t => t.IsActive == true &&
                                                        !model.Allergens.Contains(t.AllergenId));
                        if (resultAllergens != null)
                        {
                            var dietaries = _context.MenuItemDietaries
                                                .Include(p => p.Dietary)
                                                .Where(p => p.MenuItemId == item.Id);

                            var resultDietaries = dietaries.Where(t => t.IsActive == true &&
                                                        model.Dietaries.Contains(t.DietaryId));
                            if (resultDietaries != null)
                            {
                                filteredMenus += 1;
                            }
                        }
                    }
                    else if (model.Allergens.Count > 0 && model.Dietaries.Count == 0)
                    {
                        var allergens = _context.MenuItemAllergens
                                                .Include(p => p.Allergen)
                                                .Where(p => p.MenuItemId == item.Id);

                        var resultAllergens = allergens.Where(t => t.IsActive == true &&
                                                        !model.Allergens.Contains(t.AllergenId));
                        if (resultAllergens != null)
                        {
                            filteredMenus += 1;
                        }
                    }
                    else if (model.Allergens.Count == 0 && model.Dietaries.Count > 0)
                    {
                        var dietaries = _context.MenuItemDietaries
                                                .Include(p => p.Dietary)
                                                .Where(p => p.MenuItemId == item.Id);

                        var resultDietaries = dietaries.Where(t => t.IsActive == true &&
                                                    model.Dietaries.Contains(t.DietaryId));
                        if (resultDietaries != null)
                        {
                            filteredMenus += 1;
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

        //public async Task<IEnumerable<BranchModel>> GetBranchList()
        //{
        //    var BranchList = await _branchRepository.ListAllAsync();

        //    var BranchModels = ObjectMapper.Mapper.Map<IEnumerable<BranchModel>>(BranchList);

        //    return BranchModels;
        //}

        //public async Task<IPagedList<BranchModel>> SearchBranchs(PageSearchArgs args)
        //{
        //    var BranchPagedList = await _branchRepository.SearchBranchsAsync(args);

        //    //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
        //    var BranchModels = ObjectMapper.Mapper.Map<List<BranchModel>>(BranchPagedList.Items);

        //    var BranchModelPagedList = new PagedList<BranchModel>(
        //        BranchPagedList.PageIndex,
        //        BranchPagedList.PageSize,
        //        BranchPagedList.TotalCount,
        //        BranchPagedList.TotalPages,
        //        BranchModels);

        //    return BranchModelPagedList;
        //}

        //public async Task<BranchModel> GetBranchById(int BranchId)
        //{
        //    var Branch = await _branchRepository.GetByIdAsync(BranchId);

        //    var BranchModel = ObjectMapper.Mapper.Map<BranchModel>(Branch);

        //    return BranchModel;
        //}

        //public async Task<IEnumerable<BranchModel>> GetBranchsByName(string name)
        //{
        //    var spec = new BranchWithBranchesSpecification(name);
        //    var BranchList = await _branchRepository.GetAsync(spec);

        //    var BranchModels = ObjectMapper.Mapper.Map<IEnumerable<BranchModel>>(BranchList);

        //    return BranchModels;
        //}

        //public async Task<IEnumerable<BranchModel>> GetBranchsByCategoryId(int categoryId)
        //{
        //    var spec = new BranchWithBranchesSpecification(categoryId);
        //    var BranchList = await _branchRepository.GetAsync(spec);

        //    var BranchModels = ObjectMapper.Mapper.Map<IEnumerable<BranchModel>>(BranchList);

        //    return BranchModels;
        //}

        //public async Task<BranchModel> CreateBranch(BranchModel Branch)
        //{
        //    var existingBranch = await _branchRepository.GetByIdAsync(Branch.Id);
        //    if (existingBranch != null)
        //    {
        //        throw new ApplicationException("Branch with this id already exists");
        //    }

        //    var newBranch = ObjectMapper.Mapper.Map<Branch>(Branch);
        //    newBranch = await _branchRepository.SaveAsync(newBranch);

        //    _logger.LogInformation("Entity successfully added - MealmateAppService");

        //    var newBranchModel = ObjectMapper.Mapper.Map<BranchModel>(newBranch);
        //    return newBranchModel;
        //}

        //public async Task UpdateBranch(BranchModel Branch)
        //{
        //    var existingBranch = await _branchRepository.GetByIdAsync(Branch.Id);
        //    if (existingBranch == null)
        //    {
        //        throw new ApplicationException("Branch with this id is not exists");
        //    }

        //    existingBranch.Name = Branch.Name;
        //    existingBranch.Description = Branch.Description;

        //    await _branchRepository.SaveAsync(existingBranch);

        //    _logger.LogInformation("Entity successfully updated - MealmateAppService");
        //}

        //public async Task DeleteBranchById(int BranchId)
        //{
        //    var existingBranch = await _branchRepository.GetByIdAsync(BranchId);
        //    if (existingBranch == null)
        //    {
        //        throw new ApplicationException("Branch with this id is not exists");
        //    }

        //    await _branchRepository.DeleteAsync(existingBranch);

        //    _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        //}
    }
}
