using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities;
using Mealmate.Core.Interfaces;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories;
using Mealmate.Core.Specifications;
using Mealmate.Infrastructure.Paging;

namespace Mealmate.Application.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IAppLogger<BranchService> _logger;
        private readonly IMapper _mapper;

        public BranchService(
            IBranchRepository branchRepository, 
            IAppLogger<BranchService> logger, 
            IMapper mapper)
        {
            _branchRepository = branchRepository ?? throw new ArgumentNullException(nameof(branchRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<BranchModel> Create(BranchModel model)
        {
            var existingBranch = await _branchRepository.GetByIdAsync(model.Id);
            if (existingBranch != null)
            {
                throw new ApplicationException("branch with this id already exists");
            }

            var newbranch = _mapper.Map<Branch>(model);
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

            await _branchRepository.DeleteAsync(existingBranch);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<BranchModel>> Get(int restaurantId)
        {
            var result = await _branchRepository.GetAsync(x => x.RestaurantId== restaurantId);
            return _mapper.Map<IEnumerable<BranchModel>>(result);
        }

        public async Task<BranchModel> GetById(int id)
        {
            return _mapper.Map<BranchModel>(await _branchRepository.GetByIdAsync(id));
        }

        public async Task Update(BranchModel model)
        {
            var existingBranch = await _branchRepository.GetByIdAsync(model.Id);
            if (existingBranch == null)
            {
                throw new ApplicationException("Branch with this id is not exists");
            }

            existingBranch = _mapper.Map<Branch>(model);

            await _branchRepository.SaveAsync(existingBranch);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
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
