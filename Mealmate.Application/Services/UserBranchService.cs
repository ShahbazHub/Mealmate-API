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
using Mealmate.Infrastructure.Data;
using Mealmate.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace Mealmate.Application.Services
{
    public class UserBranchService : IUserBranchService
    {
        private readonly MealmateContext _context;
        private readonly IUserBranchRepository _UserBranchRepository;
        private readonly IAppLogger<UserBranchService> _logger;
        private readonly IMapper _mapper;

        public UserBranchService(
            IUserBranchRepository UserBranchRepository,
            IAppLogger<UserBranchService> logger,
            IMapper mapper,
            MealmateContext context)
        {
            _context = context;
            _UserBranchRepository = UserBranchRepository ?? throw new ArgumentNullException(nameof(UserBranchRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        #region Create
        public async Task<UserBranchModel> Create(UserBranchCreateModel model)
        {
            var newUser = new UserBranch
            {
                UserId = model.UserId,
                BranchId = model.BranchId,
                Created = DateTime.Now,
                IsActive = model.IsActive
            };

            newUser = await _UserBranchRepository.SaveAsync(newUser);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newUsermodel = _mapper.Map<UserBranchModel>(newUser);
            return newUsermodel;
        }
        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            var existingUser = await _UserBranchRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new ApplicationException("User with this id is not exists");
            }

            await _UserBranchRepository.DeleteAsync(existingUser);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }
        #endregion

        #region Read
        public async Task<IEnumerable<BranchModel>> Get(int restaurantId, int userId)
        {
            List<Branch> result = null;

            var userRestaurant = await _context.UserRestaurants
                        .FirstOrDefaultAsync(p => p.RestaurantId == restaurantId && p.UserId == userId && p.IsActive == true);

            if (userRestaurant.isOwner)
            {
                result = await _context.Branches.Where(p => p.RestaurantId == restaurantId).ToListAsync();
            }
            else
            {
                result = await _context.UserBranches
                                                    .Include(p => p.Branch)
                                                    .Where(p => p.UserId == userId && p.Branch.RestaurantId == restaurantId)
                                                    .Select(p => p.Branch)
                                                    .ToListAsync();
            }
            return _mapper.Map<IEnumerable<BranchModel>>(result);
        }

        public async Task<IEnumerable<UserBranchModel>> Get(int userId)
        {
            var result = await _UserBranchRepository.GetAsync(x => x.UserId == userId);
            return _mapper.Map<IEnumerable<UserBranchModel>>(result);
        }

        public async Task<UserBranchModel> GetById(int id)
        {
            return _mapper.Map<UserBranchModel>(await _UserBranchRepository.GetByIdAsync(id));
        }

        public async Task<IPagedList<UserBranchModel>> Search(int employeeId, PageSearchArgs args)
        {
            var TablePagedList = await _UserBranchRepository.SearchAsync(employeeId, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var BranchModels = _mapper.Map<List<UserBranchModel>>(TablePagedList.Items);

            var BranchModelPagedList = new PagedList<UserBranchModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                BranchModels);

            return BranchModelPagedList;
        }

        public async Task<IPagedList<UserModel>> List(int branchId, PageSearchArgs args)
        {
            List<UserModel> temp = new List<UserModel>();

            var TablePagedList = await _UserBranchRepository.ListAsync(branchId, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var BranchModels = _mapper.Map<List<UserBranchModel>>(TablePagedList.Items);

            foreach (var item in BranchModels)
            {
                temp.Add(new UserModel
                {
                    Created = item.User.Created,
                    Email = item.User.Email,
                    FirstName = item.User.FirstName,
                    LastName = item.User.LastName,
                    Id = item.User.Id,
                    PhoneNumber = item.User.PhoneNumber
                });
            }

            var BranchModelPagedList = new PagedList<UserModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                temp);

            return BranchModelPagedList;
        }
        #endregion

        #region Update
        public async Task Update(int id, UserBranchUpdateModel model)
        {
            var existingUser = await _UserBranchRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new ApplicationException($"Resource with this id {id} does not exists");
            }

            existingUser = _mapper.Map<UserBranch>(model);

            await _UserBranchRepository.SaveAsync(existingUser);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }
        #endregion
    }
}
