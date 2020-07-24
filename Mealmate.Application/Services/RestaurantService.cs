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
using Mealmate.Core.Specifications;
using Mealmate.Infrastructure.Paging;

namespace Mealmate.Application.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserRestaurantRepository _userRestaurantRepository;
        private readonly IAppLogger<RestaurantService> _logger;
        private readonly IMapper _mapper;

        public RestaurantService(
            IRestaurantRepository restaurantRepository,
            IUserRestaurantRepository userRestaurantRepository,
            IAppLogger<RestaurantService> logger,
            IMapper mapper)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
            _userRestaurantRepository = userRestaurantRepository ?? throw new ArgumentNullException(nameof(userRestaurantRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<RestaurantModel> Create(RestaurantCreateModel model)
        {
            var newrestaurant = new Restaurant
            {
                Description = model.Description,
                IsActive = model.IsActive,
                Name = model.Name,
                Created = DateTime.Now
            };

            newrestaurant = await _restaurantRepository.SaveAsync(newrestaurant);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newrestaurantmodel = _mapper.Map<RestaurantModel>(newrestaurant);
            return newrestaurantmodel;
        }

        public async Task<UserRestaurantModel> Create(int ownerId, RestaurantCreateModel model)
        {
            var newrestaurant = new Restaurant
            {
                Description = model.Description,
                IsActive = model.IsActive,
                Name = model.Name,
                Created = DateTime.Now
            };

            newrestaurant = await _restaurantRepository.SaveAsync(newrestaurant);

            if (newrestaurant != null)
            {
                var userRestaurant = new UserRestaurant
                {
                    Created = DateTime.Now,
                    IsActive = true,
                    OwnerId = ownerId,
                    RestaurantId = newrestaurant.Id
                };

                var result = await _userRestaurantRepository.SaveAsync(userRestaurant);

                _logger.LogInformation("Resource created successfully - mealmateappservice");

                var data = _mapper.Map<UserRestaurantModel>(result);
                return data;
            }

            throw new Exception("Error while creating resource");
        }

        public async Task Delete(int id)
        {
            var existingRestaurant = await _restaurantRepository.GetByIdAsync(id);
            if (existingRestaurant == null)
            {
                throw new ApplicationException("Restaurant with this id is not exists");
            }

            existingRestaurant.IsActive = false;
            await _restaurantRepository.SaveAsync(existingRestaurant);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IEnumerable<RestaurantModel>> Get(int ownerId)
        {
            List<RestaurantModel> result = new List<RestaurantModel>();
            var model = await _userRestaurantRepository.Search(ownerId);

            var data = _mapper.Map<IEnumerable<UserRestaurantModel>>(model);
            foreach (var item in data)
            {
                result.Add(item.Restaurant);
            }
            return result;
        }

        public async Task<RestaurantModel> GetById(int id)
        {
            return _mapper.Map<RestaurantModel>(await _restaurantRepository.GetByIdAsync(id));
        }

        public async Task<RestaurantModel> Update(int id, RestaurantUpdateModel model)
        {
            var existingRestaurant = await _restaurantRepository.GetByIdAsync(id);
            if (existingRestaurant == null)
            {
                throw new ApplicationException("Restaurant with this id is not exists");
            }

            existingRestaurant.Description = model.Description;
            existingRestaurant.IsActive = model.IsActive;
            existingRestaurant.Name = model.Name;

            var restaurantUpdated = await _restaurantRepository.SaveAsync(existingRestaurant);

            var restaurantModelUpdate = _mapper.Map<RestaurantModel>(restaurantUpdated);
            return restaurantModelUpdate;
        }

        public async Task<IPagedList<RestaurantModel>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _restaurantRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<RestaurantModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<RestaurantModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }


    }
}
