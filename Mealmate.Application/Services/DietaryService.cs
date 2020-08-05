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

        public async Task<DietaryModel> Create(DietaryCreateModel model)
        {
            var new_dietary = new Dietary
            {
                Created = DateTime.Now,
                IsActive = model.IsActive,
                Name = model.Name,
                Photo = model.Photo,
                PhotoSelected = model.PhotoSelected
            };

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

        public async Task Update(int id, DietaryUpdateModel model)
        {
            var existingTable = await _dietaryRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("Dietary with this id is not exists");
            }

            existingTable.Name = model.Name;
            existingTable.IsActive = model.IsActive;
            existingTable.Photo = model.Photo;
            existingTable.PhotoSelected = model.PhotoSelected;

            await _dietaryRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }


        public async Task<IPagedList<DietaryModel>> Search(int isActive, PageSearchArgs args)
        {
            var TablePagedList = await _dietaryRepository.SearchAsync(isActive, args);

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

    }
}
