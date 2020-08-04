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
    public class RestroomRequestStateService : IRestroomRequestStateService
    {
        private readonly IRestroomRequestStateRepository _restroomrequestStateRepository;
        private readonly IAppLogger<RestroomRequestStateService> _logger;
        private readonly IMapper _mapper;

        public RestroomRequestStateService(
            IRestroomRequestStateRepository restroomrequestStateRepository,
            IAppLogger<RestroomRequestStateService> logger,
            IMapper mapper)
        {
            _restroomrequestStateRepository = restroomrequestStateRepository ?? throw new ArgumentNullException(nameof(restroomrequestStateRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        #region Create
        public async Task<RestroomRequestStateModel> Create(RestroomRequestStateCreateModel model)
        {

            var new_dietary = new RestroomRequestState
            {
                Created = DateTime.Now,
                IsActive = model.IsActive,
                Name = model.Name
            };

            new_dietary = await _restroomrequestStateRepository.SaveAsync(new_dietary);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var new_dietarymodel = _mapper.Map<RestroomRequestStateModel>(new_dietary);
            return new_dietarymodel;
        }
        #endregion

        #region Read
        public async Task<IEnumerable<RestroomRequestStateModel>> Get()
        {
            var result = await _restroomrequestStateRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<RestroomRequestStateModel>>(result);
        }

        public async Task<RestroomRequestStateModel> GetById(int id)
        {
            RestroomRequestStateModel model = null;
            var result = await _restroomrequestStateRepository.GetByIdAsync(id);
            if (result != null)
                model = _mapper.Map<RestroomRequestStateModel>(result);
            return model;
        }
        public async Task<IPagedList<RestroomRequestStateModel>> Search(int isActive, PageSearchArgs args)
        {
            var TablePagedList = await _restroomrequestStateRepository.SearchAsync(isActive, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var RestroomRequestStateModels = _mapper.Map<List<RestroomRequestStateModel>>(TablePagedList.Items);

            var RestroomRequestStateModelPagedList = new PagedList<RestroomRequestStateModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                RestroomRequestStateModels);

            return RestroomRequestStateModelPagedList;
        }
        #endregion

        #region Update
        public async Task Update(int id, RestroomRequestStateUpdateModel model)
        {
            var existingTable = await _restroomrequestStateRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("RestroomRequestState with this id is not exists");
            }

            existingTable.Name = model.Name;
            existingTable.IsActive = model.IsActive;

            await _restroomrequestStateRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }
        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            var existingTable = await _restroomrequestStateRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("RestroomRequestState with this id is not exists");
            }

            existingTable.IsActive = false;

            await _restroomrequestStateRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        #endregion
    }
}
