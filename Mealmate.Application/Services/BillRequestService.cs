using System;
using System.Collections.Generic;
using System.Linq;
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
using Mealmate.Infrastructure.Data;
using Mealmate.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;

namespace Mealmate.Application.Services
{
    public class BillRequestService : IBillRequestService
    {
        private readonly MealmateContext _context;
        private readonly IBillRequestRepository _billRequestRepository;
        private readonly IAppLogger<BillRequestService> _logger;
        private readonly IMapper _mapper;

        public BillRequestService(
            IBillRequestRepository billRequestRepository,
            IAppLogger<BillRequestService> logger,
            IMapper mapper,
            MealmateContext context)
        {
            _context = context;
            _billRequestRepository = billRequestRepository ?? throw new ArgumentNullException(nameof(billRequestRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        #region Create
        public async Task<BillRequestModel> Create(BillRequestCreateModel model)
        {

            var new_dietary = new BillRequest
            {
                BillRequestStateId = 1,
                CustomerId = model.CustomerId,
                RequestTime = DateTime.Now,
                TableId = model.TableId
            };

            new_dietary = await _billRequestRepository.SaveAsync(new_dietary);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var new_dietarymodel = _mapper.Map<BillRequestModel>(new_dietary);
            return new_dietarymodel;
        }
        #endregion

        #region Read
        public async Task<IEnumerable<BillRequestModel>> Get()
        {
            var result = await _billRequestRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<BillRequestModel>>(result);
        }

        public async Task<IEnumerable<BillRequestModel>> Get(int restaurantId, int billRequestStateId)
        {
            var result = await _context.BillRequests
                            .Include(s => s.BillRequestState)
                            .Include(u => u.Customer)
                            .Include(p => p.Table)
                            .ThenInclude(l => l.Location)
                            .ThenInclude(b => b.Branch)
                            .ThenInclude(r => r.Restaurant)
                            .ToListAsync();
            result = result
                        .Where(p => p.Table.Location.Branch.RestaurantId == restaurantId &&
                                    p.BillRequestStateId == billRequestStateId)
                        .OrderByDescending(p => p.RequestTime)
                        .ToList();

            return _mapper.Map<IEnumerable<BillRequestModel>>(result);
        }

        public async Task<BillRequestModel> GetById(int id)
        {
            BillRequestModel model = null;
            var result = await _billRequestRepository.GetByIdAsync(id);
            if (result != null)
                model = _mapper.Map<BillRequestModel>(result);
            return model;
        }
        public async Task<IPagedList<BillRequestModel>> Search(int isActive, PageSearchArgs args)
        {
            var TablePagedList = await _billRequestRepository.SearchAsync(isActive, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var BillRequestModels = _mapper.Map<List<BillRequestModel>>(TablePagedList.Items);

            var BillRequestModelPagedList = new PagedList<BillRequestModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                BillRequestModels);

            return BillRequestModelPagedList;
        }
        #endregion

        #region Update
        public async Task Update(int id, BillRequestUpdateModel model)
        {
            var existingTable = await _billRequestRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("BillRequest with this id is not exists");
            }

            existingTable.ResponseTime = DateTime.Now;
            existingTable.Remarks = model.Remarks;
            existingTable.BillRequestStateId= model.BillRequestStateId;

            await _billRequestRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }
        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            var existingTable = await _billRequestRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("BillRequest with this id is not exists");
            }

            await _billRequestRepository.DeleteAsync(existingTable);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        #endregion
    }
}
