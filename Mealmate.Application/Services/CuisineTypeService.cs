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
    public class CuisineTypeService : ICuisineTypeService
    {
        private readonly ICuisineTypeRepository _cuisineTypeRepository;
        private readonly IAppLogger<CuisineTypeService> _logger;
        private readonly IMapper _mapper;

        public CuisineTypeService(
            ICuisineTypeRepository cuisineTypeRepository,
            IAppLogger<CuisineTypeService> logger,
            IMapper mapper)
        {
            _cuisineTypeRepository = cuisineTypeRepository ?? throw new ArgumentNullException(nameof(_cuisineTypeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<CuisineTypeModel> Create(CuisineTypeCreateModel model)
        {
            var new_dietary = new CuisineType
            {
                Created = DateTime.Now,
                IsActive = model.IsActive,
                Name = model.Name
            };

            new_dietary = await _cuisineTypeRepository.SaveAsync(new_dietary);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var new_dietarymodel = _mapper.Map<CuisineTypeModel>(new_dietary);
            return new_dietarymodel;
        }

        public async Task Delete(int id)
        {
            var existingTable = await _cuisineTypeRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("Allergen with this id is not exists");
            }

            await _cuisineTypeRepository.DeleteAsync(existingTable);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<CuisineTypeModel>> Get()
        {
            var result = await _cuisineTypeRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<CuisineTypeModel>>(result);
        }

        public async Task<CuisineTypeModel> GetById(int id)
        {
            return _mapper.Map<CuisineTypeModel>(await _cuisineTypeRepository.GetByIdAsync(id));
        }

        public async Task Update(int id, CuisineTypeUpdateModel model)
        {
            var existingTable = await _cuisineTypeRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("Allergen with this id is not exists");
            }

            existingTable.IsActive = model.IsActive;
            existingTable.Name = model.Name;

            await _cuisineTypeRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<CuisineTypeModel>> Search(int isActive, PageSearchArgs args)
        {
            var TablePagedList = await _cuisineTypeRepository.SearchAsync(isActive, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<CuisineTypeModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<CuisineTypeModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }

    }
}
