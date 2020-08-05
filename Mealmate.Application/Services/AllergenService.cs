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

        public async Task<AllergenModel> Create(AllergenCreateModel model)
        {

            var new_dietary = new Allergen
            {
                Created = DateTime.Now,
                IsActive = model.IsActive,
                Name = model.Name,
                Photo = model.Photo,
                PhotoSelected = model.PhotoSelected
            };

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

        public async Task Update(int id, AllergenUpdateModel model)
        {
            var existingTable = await _allergenRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("Allergen with this id is not exists");
            }

            existingTable.Name = model.Name;
            existingTable.IsActive = model.IsActive;
            existingTable.Photo = model.Photo;
            existingTable.PhotoSelected = model.PhotoSelected;

            await _allergenRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<AllergenModel>> Search(int isActive, PageSearchArgs args)
        {
            var TablePagedList = await _allergenRepository.SearchAsync(isActive, args);

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


    }
}
