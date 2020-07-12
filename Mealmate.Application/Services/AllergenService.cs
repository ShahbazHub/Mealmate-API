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
    public class AllergenService : IAllergenService
    {
        private readonly IAllergenRepository _allergenRepository;
        private readonly IAppLogger<AllergenService> _logger;
        private readonly IMapper _mapper;

        public AllergenService(
            IAllergenRepository allergenRepository,
            IAppLogger<AllergenService> logger,
            IMapper mapper)
        {
            _allergenRepository = allergenRepository ?? throw new ArgumentNullException(nameof(_allergenRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<AllergenModel> Create(AllergenModel model)
        {
            var existingTable = await _allergenRepository.GetByIdAsync(model.Id);
            if (existingTable != null)
            {
                throw new ApplicationException("_dietary with this id already exists");
            }

            var new_dietary = _mapper.Map<Allergen>(model);
            new_dietary = await _allergenRepository.SaveAsync(new_dietary);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var new_dietarymodel = _mapper.Map<AllergenModel>(new_dietary);
            return new_dietarymodel;
        }

        public async Task Delete(int id)
        {
            var existingTable = await _allergenRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("Allergen with this id is not exists");
            }

            await _allergenRepository.DeleteAsync(existingTable);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<AllergenModel>> Get()
        {
            var result = await _allergenRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<AllergenModel>>(result);
        }

        public async Task<AllergenModel> GetById(int id)
        {
            return _mapper.Map<AllergenModel>(await _allergenRepository.GetByIdAsync(id));
        }

        public async Task Update(AllergenModel model)
        {
            var existingTable = await _allergenRepository.GetByIdAsync(model.Id);
            if (existingTable == null)
            {
                throw new ApplicationException("Allergen with this id is not exists");
            }

            existingTable = _mapper.Map<Allergen>(model);

            await _allergenRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        //public async Task<IEnumerable<AllergenModel>> GetTableList()
        //{
        //    var TableList = await _allergenRepository.ListAllAsync();

        //    var AllergenModels = ObjectMapper.Mapper.Map<IEnumerable<AllergenModel>>(TableList);

        //    return AllergenModels;
        //}

        public async Task<IPagedList<AllergenModel>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _allergenRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<AllergenModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<AllergenModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }

        //public async Task<AllergenModel> GetTableById(int TableId)
        //{
        //    var Allergen = await _allergenRepository.GetByIdAsync(TableId);

        //    var AllergenModel = ObjectMapper.Mapper.Map<AllergenModel>(Allergen);

        //    return AllergenModel;
        //}

        //public async Task<IEnumerable<AllergenModel>> GetTablesByName(string name)
        //{
        //    var spec = new TableWithTableesSpecification(name);
        //    var TableList = await _allergenRepository.GetAsync(spec);

        //    var AllergenModels = ObjectMapper.Mapper.Map<IEnumerable<AllergenModel>>(TableList);

        //    return AllergenModels;
        //}

        //public async Task<IEnumerable<AllergenModel>> GetTablesByCategoryId(int categoryId)
        //{
        //    var spec = new TableWithTableesSpecification(categoryId);
        //    var TableList = await _allergenRepository.GetAsync(spec);

        //    var AllergenModels = ObjectMapper.Mapper.Map<IEnumerable<AllergenModel>>(TableList);

        //    return AllergenModels;
        //}

        //public async Task<AllergenModel> CreateTable(AllergenModel Allergen)
        //{
        //    var existingTable = await _allergenRepository.GetByIdAsync(Allergen.Id);
        //    if (existingTable != null)
        //    {
        //        throw new ApplicationException("Allergen with this id already exists");
        //    }

        //    var newTable = ObjectMapper.Mapper.Map<Allergen>(Allergen);
        //    newTable = await _allergenRepository.SaveAsync(newTable);

        //    _logger.LogInformation("Entity successfully added - MealmateAppService");

        //    var newAllergenModel = ObjectMapper.Mapper.Map<AllergenModel>(newTable);
        //    return newAllergenModel;
        //}

        //public async Task UpdateTable(AllergenModel Allergen)
        //{
        //    var existingTable = await _allergenRepository.GetByIdAsync(Allergen.Id);
        //    if (existingTable == null)
        //    {
        //        throw new ApplicationException("Allergen with this id is not exists");
        //    }

        //    existingTable.Name = Allergen.Name;
        //    existingTable.Description = Allergen.Description;

        //    await _allergenRepository.SaveAsync(existingTable);

        //    _logger.LogInformation("Entity successfully updated - MealmateAppService");
        //}

        //public async Task DeleteTableById(int TableId)
        //{
        //    var existingTable = await _allergenRepository.GetByIdAsync(TableId);
        //    if (existingTable == null)
        //    {
        //        throw new ApplicationException("Allergen with this id is not exists");
        //    }

        //    await _allergenRepository.DeleteAsync(existingTable);

        //    _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        //}
    }
}
