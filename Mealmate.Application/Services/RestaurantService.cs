using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Mapper;
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
        private readonly IRestaurantRepository _RestaurantRepository;
        private readonly IAppLogger<RestaurantService> _logger;

        public RestaurantService(IRestaurantRepository RestaurantRepository, IAppLogger<RestaurantService> logger)
        {
            _RestaurantRepository = RestaurantRepository ?? throw new ArgumentNullException(nameof(RestaurantRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<RestaurantModel>> GetRestaurantList()
        {
            var RestaurantList = await _RestaurantRepository.ListAllAsync();

            var RestaurantModels = ObjectMapper.Mapper.Map<IEnumerable<RestaurantModel>>(RestaurantList);

            return RestaurantModels;
        }

        public async Task<IPagedList<RestaurantModel>> SearchRestaurants(PageSearchArgs args)
        {
            var RestaurantPagedList = await _RestaurantRepository.SearchRestaurantsAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var RestaurantModels = ObjectMapper.Mapper.Map<List<RestaurantModel>>(RestaurantPagedList.Items);

            var RestaurantModelPagedList = new PagedList<RestaurantModel>(
                RestaurantPagedList.PageIndex,
                RestaurantPagedList.PageSize,
                RestaurantPagedList.TotalCount,
                RestaurantPagedList.TotalPages,
                RestaurantModels);

            return RestaurantModelPagedList;
        }

        public async Task<RestaurantModel> GetRestaurantById(int RestaurantId)
        {
            var Restaurant = await _RestaurantRepository.GetByIdAsync(RestaurantId);

            var RestaurantModel = ObjectMapper.Mapper.Map<RestaurantModel>(Restaurant);

            return RestaurantModel;
        }

        public async Task<IEnumerable<RestaurantModel>> GetRestaurantsByName(string name)
        {
            var spec = new RestaurantWithBranchesSpecification(name);
            var RestaurantList = await _RestaurantRepository.GetAsync(spec);

            var RestaurantModels = ObjectMapper.Mapper.Map<IEnumerable<RestaurantModel>>(RestaurantList);

            return RestaurantModels;
        }

        public async Task<IEnumerable<RestaurantModel>> GetRestaurantsByCategoryId(int categoryId)
        {
            var spec = new RestaurantWithBranchesSpecification(categoryId);
            var RestaurantList = await _RestaurantRepository.GetAsync(spec);

            var RestaurantModels = ObjectMapper.Mapper.Map<IEnumerable<RestaurantModel>>(RestaurantList);

            return RestaurantModels;
        }

        public async Task<RestaurantModel> CreateRestaurant(RestaurantModel Restaurant)
        {
            var existingRestaurant = await _RestaurantRepository.GetByIdAsync(Restaurant.Id);
            if (existingRestaurant != null)
            {
                throw new ApplicationException("Restaurant with this id already exists");
            }

            var newRestaurant = ObjectMapper.Mapper.Map<Restaurant>(Restaurant);
            newRestaurant = await _RestaurantRepository.SaveAsync(newRestaurant);

            _logger.LogInformation("Entity successfully added - MealmateAppService");

            var newRestaurantModel = ObjectMapper.Mapper.Map<RestaurantModel>(newRestaurant);
            return newRestaurantModel;
        }

        public async Task UpdateRestaurant(RestaurantModel Restaurant)
        {
            var existingRestaurant = await _RestaurantRepository.GetByIdAsync(Restaurant.Id);
            if (existingRestaurant == null)
            {
                throw new ApplicationException("Restaurant with this id is not exists");
            }

            existingRestaurant.Name = Restaurant.Name;
            existingRestaurant.Description = Restaurant.Description;

            await _RestaurantRepository.SaveAsync(existingRestaurant);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task DeleteRestaurantById(int RestaurantId)
        {
            var existingRestaurant = await _RestaurantRepository.GetByIdAsync(RestaurantId);
            if (existingRestaurant == null)
            {
                throw new ApplicationException("Restaurant with this id is not exists");
            }

            await _RestaurantRepository.DeleteAsync(existingRestaurant);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }
    }
}
