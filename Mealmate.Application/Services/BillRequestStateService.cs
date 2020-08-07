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
    public class BillRequestStateService : IBillRequestStateService
    {
        private readonly IBillRequestStateRepository _billRequestStateRepository;
        private readonly IAppLogger<BillRequestStateService> _logger;
        private readonly IMapper _mapper;

        public BillRequestStateService(
            IBillRequestStateRepository billRequestStateRepository,
            IAppLogger<BillRequestStateService> logger,
            IMapper mapper)
        {
            _billRequestStateRepository = billRequestStateRepository ?? throw new ArgumentNullException(nameof(billRequestStateRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        #region Create
        public async Task<BillRequestStateModel> Create(BillRequestStateCreateModel model)
        {

            var new_dietary = new BillRequestState
            {
                Created = DateTime.Now,
                IsActive = model.IsActive,
                Name = model.Name
            };

            new_dietary = await _billRequestStateRepository.SaveAsync(new_dietary);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var new_dietarymodel = _mapper.Map<BillRequestStateModel>(new_dietary);
            return new_dietarymodel;
        }
        #endregion

        #region Read
        public async Task<IEnumerable<BillRequestStateModel>> Get()
        {
            var result = await _billRequestStateRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<BillRequestStateModel>>(result);
        }

        public async Task<BillRequestStateModel> GetById(int id)
        {
            BillRequestStateModel model = null;
            var result = await _billRequestStateRepository.GetByIdAsync(id);
            if (result != null)
                model = _mapper.Map<BillRequestStateModel>(result);
            return model;
        }
        public async Task<IPagedList<BillRequestStateModel>> Search(int isActive, PageSearchArgs args)
        {
            var TablePagedList = await _billRequestStateRepository.SearchAsync(isActive, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var BillRequestStateModels = _mapper.Map<List<BillRequestStateModel>>(TablePagedList.Items);

            var BillRequestStateModelPagedList = new PagedList<BillRequestStateModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                BillRequestStateModels);

            return BillRequestStateModelPagedList;
        }
        #endregion

        #region Update
        public async Task Update(int id, BillRequestStateUpdateModel model)
        {
            var existingTable = await _billRequestStateRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("BillRequestState with this id is not exists");
            }

            existingTable.Name = model.Name;
            existingTable.IsActive = model.IsActive;

            await _billRequestStateRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }
        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            var existingTable = await _billRequestStateRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("BillRequestState with this id is not exists");
            }

            existingTable.IsActive = false;

            await _billRequestStateRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        #endregion
    }
}
