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
using Mealmate.Infrastructure.Data;
using Mealmate.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Mealmate.Application.Services
{
    public class ContactRequestService : IContactRequestService
    {
        private readonly MealmateContext _context;
        private readonly IContactRequestRepository _contactRequestRepository;
        private readonly IAppLogger<ContactRequestService> _logger;
        private readonly IMapper _mapper;

        public ContactRequestService(
            IContactRequestRepository contactRequestRepository,
            IAppLogger<ContactRequestService> logger,
            IMapper mapper,
            MealmateContext context)
        {
            _context = context;
            _contactRequestRepository = contactRequestRepository ?? throw new ArgumentNullException(nameof(contactRequestRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        #region Create
        public async Task<ContactRequestModel> Create(ContactRequestCreateModel model)
        {

            // 1: new request creation
            var new_dietary = new ContactRequest
            {
                CustomerId = model.CustomerId,
                TableId = model.TableId,
                RequestTime = DateTime.Now,
                ContactRequestStateId = 1,
                ResponseTime = null
            };

            new_dietary = await _contactRequestRepository.SaveAsync(new_dietary);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var new_dietarymodel = _mapper.Map<ContactRequestModel>(new_dietary);
            return new_dietarymodel;
        }
        #endregion

        #region Read
        public async Task<IEnumerable<ContactRequestModel>> Get()
        {
            var result = await _contactRequestRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<ContactRequestModel>>(result);
        }

        public async Task<IEnumerable<ContactRequestModel>> Get(int branchId, int contactRequestStateId)
        {
            var result = await _context.ContactRequests
                            .Include(s => s.ContactRequestState)
                            .Include(u => u.Customer)
                            .Include(p => p.Table)
                            .ThenInclude(l => l.Location)
                            .ThenInclude(b => b.Branch)
                            .ThenInclude(r => r.Restaurant)
                            .ToListAsync();
            result = result
                        .Where(p => p.Table.Location.BranchId == branchId &&
                                    p.ContactRequestStateId == contactRequestStateId)
                        .OrderByDescending(p => p.RequestTime)
                        .ToList();

            return _mapper.Map<IEnumerable<ContactRequestModel>>(result);
        }

        public async Task<ContactRequestModel> GetById(int id)
        {
            ContactRequestModel model = null;
            var result = await _contactRequestRepository.GetByIdAsync(id);
            if (result != null)
                model = _mapper.Map<ContactRequestModel>(result);
            return model;
        }
        public async Task<IPagedList<ContactRequestModel>> Search(int branchId, PageSearchArgs args)
        {
            var TablePagedList = await _contactRequestRepository.SearchAsync(branchId, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var ContactRequestModels = _mapper.Map<List<ContactRequestModel>>(TablePagedList.Items);

            var ContactRequestModelPagedList = new PagedList<ContactRequestModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                ContactRequestModels);

            return ContactRequestModelPagedList;
        }
        #endregion

        #region Update
        public async Task Update(int id, ContactRequestUpdateModel model)
        {
            var existingTable = await _contactRequestRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("ContactRequest with this id is not exists");
            }

            existingTable.Remarks = model.Remarks;
            existingTable.ResponseTime = DateTime.Now;
            existingTable.ContactRequestStateId = model.ContactRequestStateId;

            await _contactRequestRepository.SaveAsync(existingTable);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }
        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            var existingTable = await _contactRequestRepository.GetByIdAsync(id);
            if (existingTable == null)
            {
                throw new ApplicationException("ContactRequest with this id is not exists");
            }

            await _contactRequestRepository.DeleteAsync(existingTable);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        #endregion
    }
}
