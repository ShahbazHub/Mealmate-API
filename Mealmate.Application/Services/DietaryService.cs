using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities;
using Mealmate.Core.Entities.Lookup;
using Mealmate.Core.Interfaces;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories;
using Mealmate.Core.Specifications;
using Mealmate.Infrastructure.Paging;

namespace Mealmate.Application.Services
{
    public class DietaryService : IDietaryService
    {
        private readonly IDietaryRepository _dietaryRepository;
        private readonly IAppLogger<DietaryService> _logger;
        private readonly IMapper _mapper;

        public DietaryService(
            IDietaryRepository dietaryRepository,
            IAppLogger<DietaryService> logger,
            IMapper mapper)
        {
            _dietaryRepository = dietaryRepository ?? throw new ArgumentNullException(nameof(_dietaryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<DietaryModel> Create(DietaryModel model)
        {
            var existingTable = await _dietaryRepository.GetByIdAsync(model.Id);
            if (existingTable != null)
            {
                throw new ApplicationException("_dietary with this id already exists");
            }

            var new_dietary = _mapper.Map<Dietary>(model);
            new_dietary = await _dietaryRepository.SaveAsync(new_dietary);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var new_dietarymodel = _mapper.Map<DietaryModel>(new_dietary);
            return new_dietarymodel;
        }

        public async Task Delete(int id)
        {
            var existingTable = await _dietaryRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("Dietary with this id is not exists");
            }

            await _dietaryRepository.DeleteAsync(existingTable);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<DietaryModel>> Get()
        {
            var result = await _dietaryRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<DietaryModel>>(result);
        }

        public async Task<DietaryModel> GetById(int id)
        {
            return _mapper.Map<DietaryModel>(await _dietaryRepository.GetByIdAsync(id));
        }

        public async Task Update(DietaryModel model)
        {
            var existingTable = await _dietaryRepository.GetByIdAsync(model.Id);
            if (existingTable == null)
            {
                throw new ApplicationException("Dietary with this id is not exists");
            }

            existingTable = _mapper.Map<Dietary>(model);

            await _dietaryRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }


        public async Task<IPagedList<DietaryModel>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _dietaryRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var tempModels = _mapper.Map<List<DietaryModel>>(TablePagedList.Items);

            var tempModelPagedList = new PagedList<DietaryModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                tempModels);

            return tempModelPagedList;
        }
        //public async Task<IEnumerable<DietaryModel>> GetTableList()
        //{
        //    var TableList = await _dietaryRepository.ListAllAsync();

        //    var DietaryModels = ObjectMapper.Mapper.Map<IEnumerable<DietaryModel>>(TableList);

        //    return DietaryModels;
        //}

        //public async Task<IPagedList<DietaryModel>> SearchTables(PageSearchArgs args)
        //{
        //    var TablePagedList = await _dietaryRepository.SearchTablesAsync(args);

        //    //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
        //    var DietaryModels = ObjectMapper.Mapper.Map<List<DietaryModel>>(TablePagedList.Items);

        //    var DietaryModelPagedList = new PagedList<DietaryModel>(
        //        TablePagedList.PageIndex,
        //        TablePagedList.PageSize,
        //        TablePagedList.TotalCount,
        //        TablePagedList.TotalPages,
        //        DietaryModels);

        //    return DietaryModelPagedList;
        //}

        //public async Task<DietaryModel> GetTableById(int TableId)
        //{
        //    var Dietary = await _dietaryRepository.GetByIdAsync(TableId);

        //    var DietaryModel = ObjectMapper.Mapper.Map<DietaryModel>(Dietary);

        //    return DietaryModel;
        //}

        //public async Task<IEnumerable<DietaryModel>> GetTablesByName(string name)
        //{
        //    var spec = new TableWithTableesSpecification(name);
        //    var TableList = await _dietaryRepository.GetAsync(spec);

        //    var DietaryModels = ObjectMapper.Mapper.Map<IEnumerable<DietaryModel>>(TableList);

        //    return DietaryModels;
        //}

        //public async Task<IEnumerable<DietaryModel>> GetTablesByCategoryId(int categoryId)
        //{
        //    var spec = new TableWithTableesSpecification(categoryId);
        //    var TableList = await _dietaryRepository.GetAsync(spec);

        //    var DietaryModels = ObjectMapper.Mapper.Map<IEnumerable<DietaryModel>>(TableList);

        //    return DietaryModels;
        //}

        //public async Task<DietaryModel> CreateTable(DietaryModel Dietary)
        //{
        //    var existingTable = await _dietaryRepository.GetByIdAsync(Dietary.Id);
        //    if (existingTable != null)
        //    {
        //        throw new ApplicationException("Dietary with this id already exists");
        //    }

        //    var newTable = ObjectMapper.Mapper.Map<Dietary>(Dietary);
        //    newTable = await _dietaryRepository.SaveAsync(newTable);

        //    _logger.LogInformation("Entity successfully added - MealmateAppService");

        //    var newDietaryModel = ObjectMapper.Mapper.Map<DietaryModel>(newTable);
        //    return newDietaryModel;
        //}

        //public async Task UpdateTable(DietaryModel Dietary)
        //{
        //    var existingTable = await _dietaryRepository.GetByIdAsync(Dietary.Id);
        //    if (existingTable == null)
        //    {
        //        throw new ApplicationException("Dietary with this id is not exists");
        //    }

        //    existingTable.Name = Dietary.Name;
        //    existingTable.Description = Dietary.Description;

        //    await _dietaryRepository.SaveAsync(existingTable);

        //    _logger.LogInformation("Entity successfully updated - MealmateAppService");
        //}

        //public async Task DeleteTableById(int TableId)
        //{
        //    var existingTable = await _dietaryRepository.GetByIdAsync(TableId);
        //    if (existingTable == null)
        //    {
        //        throw new ApplicationException("Dietary with this id is not exists");
        //    }

        //    await _dietaryRepository.DeleteAsync(existingTable);

        //    _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        //}
    }
}
