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
    public class OptionItemDietaryService : IOptionItemDietaryService
    {
        private readonly IOptionItemDietaryRepository _optionItemDietaryRepository;
        private readonly IAppLogger<OptionItemDietaryService> _logger;
        private readonly IMapper _mapper;

        public OptionItemDietaryService(
            IOptionItemDietaryRepository optionItemDietaryRepository,
            IAppLogger<OptionItemDietaryService> logger,
            IMapper mapper)
        {
            _optionItemDietaryRepository = optionItemDietaryRepository ?? throw new ArgumentNullException(nameof(optionItemDietaryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<OptionItemDietaryModel> Create(OptionItemDietaryCreateModel model)
        {
            var newoptionItem = new OptionItemDietary
            {
                Created = DateTime.Now,
                DietaryId = model.DietaryId,
                IsActive = model.IsActive,
                OptionItemId = model.OptionItemId
            };

            newoptionItem = await _optionItemDietaryRepository.SaveAsync(newoptionItem);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newoptionItemmodel = _mapper.Map<OptionItemDietaryModel>(newoptionItem);
            return newoptionItemmodel;
        }

        public async Task Delete(int id)
        {
            var existingOptionItem = await _optionItemDietaryRepository.GetByIdAsync(id);
            if (existingOptionItem == null)
            {
                throw new ApplicationException("OptionItem with this id is not exists");
            }

            await _optionItemDietaryRepository.DeleteAsync(existingOptionItem);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<OptionItemDietaryModel>> Get(int optionItemId)
        {
            var result = await _optionItemDietaryRepository.GetAsync(x => x.OptionItemId == optionItemId);
            return _mapper.Map<IEnumerable<OptionItemDietaryModel>>(result);
        }

        public async Task<OptionItemDietaryModel> GetById(int id)
        {
            return _mapper.Map<OptionItemDietaryModel>(await _optionItemDietaryRepository.GetByIdAsync(id));
        }

        public async Task Update(int id, OptionItemDietaryUpdateModel model)
        {
            var existingOptionItem = await _optionItemDietaryRepository.GetByIdAsync(id);
            if (existingOptionItem == null)
            {
                throw new ApplicationException("OptionItem with this id is not exists");
            }

            existingOptionItem.IsActive = model.IsActive;

            await _optionItemDietaryRepository.SaveAsync(existingOptionItem);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<OptionItemDietaryModel>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _optionItemDietaryRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var DietaryModels = _mapper.Map<List<OptionItemDietaryModel>>(TablePagedList.Items);

            var DietaryModelPagedList = new PagedList<OptionItemDietaryModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                DietaryModels);

            return DietaryModelPagedList;
        }

        public async Task<IPagedList<OptionItemDietaryModel>> Search(int optionItemId, int isActive, PageSearchArgs args)
        {
            var TablePagedList = await _optionItemDietaryRepository.SearchAsync(optionItemId, isActive, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var DietaryModels = _mapper.Map<List<OptionItemDietaryModel>>(TablePagedList.Items);

            var DietaryModelPagedList = new PagedList<OptionItemDietaryModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                DietaryModels);

            return DietaryModelPagedList;
        }
    }
}
