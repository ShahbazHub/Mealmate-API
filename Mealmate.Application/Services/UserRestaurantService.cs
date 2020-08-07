using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities;
using Mealmate.Core.Interfaces;
using Mealmate.Core.Paging;
using Mealmate.Core.Repositories;
using Mealmate.Infrastructure.Paging;

namespace Mealmate.Application.Services
{
    public class UserRestaurantService : IUserRestaurantService
    {
        private readonly IUserRestaurantRepository _UserRestaurantRepository;
        private readonly IAppLogger<UserRestaurantService> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantService _restaurantService;
        private readonly IBranchService _branchService;

        public UserRestaurantService(
            IUserRestaurantRepository UserRestaurantRepository,
            IAppLogger<UserRestaurantService> logger,
            IMapper mapper,
            IRestaurantService restaurantService,
            IBranchService branchService)
        {
            _restaurantService = restaurantService;
            _branchService = branchService;
            _UserRestaurantRepository = UserRestaurantRepository ?? throw new ArgumentNullException(nameof(UserRestaurantRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<UserRestaurantModel> Create(UserRestaurantCreateModel model)
        {
            var newUser = new UserRestaurant
            {
                UserId = model.UserId,
                RestaurantId = model.RestaurantId,
                Created = DateTime.Now,
                IsActive = model.IsActive,
                isOwner = model.IsOwner
            };

            newUser = await _UserRestaurantRepository.SaveAsync(newUser);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newUsermodel = _mapper.Map<UserRestaurantModel>(newUser);
            return newUsermodel;
        }

        public async Task Delete(int id)
        {
            var existingUser = await _UserRestaurantRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new ApplicationException("User with this id is not exists");
            }

            await _UserRestaurantRepository.DeleteAsync(existingUser);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<UserRestaurantModel>> Get(int userId)
        {
            var result = await _UserRestaurantRepository.GetAsync(x => x.UserId == userId);
            return _mapper.Map<IEnumerable<UserRestaurantModel>>(result);
        }

        public async Task<UserRestaurantModel> GetById(int id)
        {
            return _mapper.Map<UserRestaurantModel>(await _UserRestaurantRepository.GetByIdAsync(id));
        }

        public async Task Update(int id, UserRestaurantUpdateModel model)
        {
            var existingUser = await _UserRestaurantRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new ApplicationException($"Resource with this id {id} does not exists");
            }

            existingUser = _mapper.Map<UserRestaurant>(model);

            await _UserRestaurantRepository.SaveAsync(existingUser);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<UserRestaurantModel>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _UserRestaurantRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var RestaurantModels = _mapper.Map<List<UserRestaurantModel>>(TablePagedList.Items);

            var RestaurantModelPagedList = new PagedList<UserRestaurantModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                RestaurantModels);

            return RestaurantModelPagedList;
        }

        public async Task<IPagedList<UserRestaurantModel>> Search(int userId, PageSearchArgs args)
        {
            var TablePagedList = await _UserRestaurantRepository.SearchAsync(userId, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var RestaurantModels = _mapper.Map<List<UserRestaurantModel>>(TablePagedList.Items);

            var RestaurantModelPagedList = new PagedList<UserRestaurantModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                RestaurantModels);

            return RestaurantModelPagedList;
        }

        public async Task<IPagedList<UserModel>> List(int restaurantId, PageSearchArgs args)
        {
            List<UserModel> temp = new List<UserModel>();

            var TablePagedList = await _UserRestaurantRepository.ListAsync(restaurantId, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var RestaurantModels = _mapper.Map<List<UserRestaurantModel>>(TablePagedList.Items);

            foreach (var item in RestaurantModels)
            {
                var user = new UserModel
                {
                    Created = item.User.Created,
                    Email = item.User.Email,
                    FirstName = item.User.FirstName,
                    LastName = item.User.LastName,
                    Id = item.User.Id,
                    PhoneNumber = item.User.PhoneNumber
                };

                var restaurants = await _restaurantService.Get(user.Id);
                user.Restaurants = restaurants.ToList();

                var branches = await _branchService.GetByEmployee(user.Id);
                user.Branches = branches.ToList();

                temp.Add(user);
            }

            var RestaurantModelPagedList = new PagedList<UserModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                temp);

            return RestaurantModelPagedList;
        }

    }
}
