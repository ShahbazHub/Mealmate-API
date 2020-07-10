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
    public class OptionItemService : IOptionItemService
    {
        private readonly IOptionItemRepository _optionItemRepository;
        private readonly IAppLogger<OptionItemService> _logger;
        private readonly IMapper _mapper;

        public OptionItemService(
            IOptionItemRepository optionItemRepository, 
            IAppLogger<OptionItemService> logger, 
            IMapper mapper)
        {
            _optionItemRepository = optionItemRepository ?? throw new ArgumentNullException(nameof(optionItemRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<OptionItemModel> Create(OptionItemModel model)
        {
            var existingOptionItem = await _optionItemRepository.GetByIdAsync(model.Id);
            if (existingOptionItem != null)
            {
                throw new ApplicationException("optionItem with this id already exists");
            }

            var newoptionItem = _mapper.Map<OptionItem>(model);
            newoptionItem = await _optionItemRepository.SaveAsync(newoptionItem);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newoptionItemmodel = _mapper.Map<OptionItemModel>(newoptionItem);
            return newoptionItemmodel;
        }

        public async Task Delete(int id)
        {
            var existingOptionItem = await _optionItemRepository.GetByIdAsync(id);
            if (existingOptionItem == null)
            {
                throw new ApplicationException("OptionItem with this id is not exists");
            }

            await _optionItemRepository.DeleteAsync(existingOptionItem);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<OptionItemModel>> Get()
        {
            var result = await _optionItemRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<OptionItemModel>>(result);
        }

        public async Task<OptionItemModel> GetById(int id)
        {
            return _mapper.Map<OptionItemModel>(await _optionItemRepository.GetByIdAsync(id));
        }

        public async Task Update(OptionItemModel model)
        {
            var existingOptionItem = await _optionItemRepository.GetByIdAsync(model.Id);
            if (existingOptionItem == null)
            {
                throw new ApplicationException("OptionItem with this id is not exists");
            }

            existingOptionItem = _mapper.Map<OptionItem>(model);

            await _optionItemRepository.SaveAsync(existingOptionItem);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        //public async Task<IEnumerable<OptionItemModel>> GetOptionItemList()
        //{
        //    var OptionItemList = await _optionItemRepository.ListAllAsync();

        //    var OptionItemModels = ObjectMapper.Mapper.Map<IEnumerable<OptionItemModel>>(OptionItemList);

        //    return OptionItemModels;
        //}

        //public async Task<IPagedList<OptionItemModel>> SearchOptionItems(PageSearchArgs args)
        //{
        //    var OptionItemPagedList = await _optionItemRepository.SearchOptionItemsAsync(args);

        //    //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
        //    var OptionItemModels = ObjectMapper.Mapper.Map<List<OptionItemModel>>(OptionItemPagedList.Items);

        //    var OptionItemModelPagedList = new PagedList<OptionItemModel>(
        //        OptionItemPagedList.PageIndex,
        //        OptionItemPagedList.PageSize,
        //        OptionItemPagedList.TotalCount,
        //        OptionItemPagedList.TotalPages,
        //        OptionItemModels);

        //    return OptionItemModelPagedList;
        //}

        //public async Task<OptionItemModel> GetOptionItemById(int OptionItemId)
        //{
        //    var OptionItem = await _optionItemRepository.GetByIdAsync(OptionItemId);

        //    var OptionItemModel = ObjectMapper.Mapper.Map<OptionItemModel>(OptionItem);

        //    return OptionItemModel;
        //}

        //public async Task<IEnumerable<OptionItemModel>> GetOptionItemsByName(string name)
        //{
        //    var spec = new OptionItemWithOptionItemesSpecification(name);
        //    var OptionItemList = await _optionItemRepository.GetAsync(spec);

        //    var OptionItemModels = ObjectMapper.Mapper.Map<IEnumerable<OptionItemModel>>(OptionItemList);

        //    return OptionItemModels;
        //}

        //public async Task<IEnumerable<OptionItemModel>> GetOptionItemsByCategoryId(int categoryId)
        //{
        //    var spec = new OptionItemWithOptionItemesSpecification(categoryId);
        //    var OptionItemList = await _optionItemRepository.GetAsync(spec);

        //    var OptionItemModels = ObjectMapper.Mapper.Map<IEnumerable<OptionItemModel>>(OptionItemList);

        //    return OptionItemModels;
        //}

        //public async Task<OptionItemModel> CreateOptionItem(OptionItemModel OptionItem)
        //{
        //    var existingOptionItem = await _optionItemRepository.GetByIdAsync(OptionItem.Id);
        //    if (existingOptionItem != null)
        //    {
        //        throw new ApplicationException("OptionItem with this id already exists");
        //    }

        //    var newOptionItem = ObjectMapper.Mapper.Map<OptionItem>(OptionItem);
        //    newOptionItem = await _optionItemRepository.SaveAsync(newOptionItem);

        //    _logger.LogInformation("Entity successfully added - MealmateAppService");

        //    var newOptionItemModel = ObjectMapper.Mapper.Map<OptionItemModel>(newOptionItem);
        //    return newOptionItemModel;
        //}

        //public async Task UpdateOptionItem(OptionItemModel OptionItem)
        //{
        //    var existingOptionItem = await _optionItemRepository.GetByIdAsync(OptionItem.Id);
        //    if (existingOptionItem == null)
        //    {
        //        throw new ApplicationException("OptionItem with this id is not exists");
        //    }

        //    existingOptionItem.Name = OptionItem.Name;
        //    existingOptionItem.Description = OptionItem.Description;

        //    await _optionItemRepository.SaveAsync(existingOptionItem);

        //    _logger.LogInformation("Entity successfully updated - MealmateAppService");
        //}

        //public async Task DeleteOptionItemById(int OptionItemId)
        //{
        //    var existingOptionItem = await _optionItemRepository.GetByIdAsync(OptionItemId);
        //    if (existingOptionItem == null)
        //    {
        //        throw new ApplicationException("OptionItem with this id is not exists");
        //    }

        //    await _optionItemRepository.DeleteAsync(existingOptionItem);

        //    _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        //}
    }
}
