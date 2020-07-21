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
        private readonly IAppLogger<RestaurantService> _logger;
        private readonly IMapper _mapper;

        public RestaurantService(
            IRestaurantRepository restaurantRepository,
            IAppLogger<RestaurantService> logger,
            IMapper mapper)
        {
            _restaurantRepository = restaurantRepository ?? throw new ArgumentNullException(nameof(restaurantRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<RestaurantModel> Create(RestaurantModel model)
        {
            var newrestaurant = _mapper.Map<Restaurant>(model);
            newrestaurant = await _restaurantRepository.SaveAsync(newrestaurant);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newrestaurantmodel = _mapper.Map<RestaurantModel>(newrestaurant);
            return newrestaurantmodel;
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

            var result = await _restaurantRepository.GetRestaurantWithBranchesByOwnerIdAsync(ownerId);
            return _mapper.Map<IEnumerable<RestaurantModel>>(result);
        }

        public async Task<RestaurantModel> GetById(int id)
        {
            return _mapper.Map<RestaurantModel>(await _restaurantRepository.GetByIdAsync(id));
        }

        public async Task<RestaurantModel> Update(RestaurantModel model)
        {
            var existingRestaurant = await _restaurantRepository.GetByIdAsync(model.Id);
            if (existingRestaurant == null)
            {
                throw new ApplicationException("Restaurant with this id is not exists");
            }


            existingRestaurant = _mapper.Map<Restaurant>(model);
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
        //public async Task<IEnumerable<RestaurantModel>> GetRestaurantList()
        //{
        //    var RestaurantList = await _restaurantRepository.ListAllAsync();

        //    var RestaurantModels = ObjectMapper.Mapper.Map<IEnumerable<RestaurantModel>>(RestaurantList);

        //    return RestaurantModels;
        //}

        //public async Task<IPagedList<RestaurantModel>> SearchRestaurants(PageSearchArgs args)
        //{
        //    var RestaurantPagedList = await _restaurantRepository.SearchRestaurantsAsync(args);

        //    //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
        //    var RestaurantModels = ObjectMapper.Mapper.Map<List<RestaurantModel>>(RestaurantPagedList.Items);

        //    var RestaurantModelPagedList = new PagedList<RestaurantModel>(
        //        RestaurantPagedList.PageIndex,
        //        RestaurantPagedList.PageSize,
        //        RestaurantPagedList.TotalCount,
        //        RestaurantPagedList.TotalPages,
        //        RestaurantModels);

        //    return RestaurantModelPagedList;
        //}

        //public async Task<RestaurantModel> GetRestaurantById(int RestaurantId)
        //{
        //    var Restaurant = await _restaurantRepository.GetByIdAsync(RestaurantId);

        //    var RestaurantModel = ObjectMapper.Mapper.Map<RestaurantModel>(Restaurant);

        //    return RestaurantModel;
        //}

        //public async Task<IEnumerable<RestaurantModel>> GetRestaurantsByName(string name)
        //{
        //    var spec = new RestaurantWithBranchesSpecification(name);
        //    var RestaurantList = await _restaurantRepository.GetAsync(spec);

        //    var RestaurantModels = ObjectMapper.Mapper.Map<IEnumerable<RestaurantModel>>(RestaurantList);

        //    return RestaurantModels;
        //}

        //public async Task<IEnumerable<RestaurantModel>> GetRestaurantsByCategoryId(int categoryId)
        //{
        //    var spec = new RestaurantWithBranchesSpecification(categoryId);
        //    var RestaurantList = await _restaurantRepository.GetAsync(spec);

        //    var RestaurantModels = ObjectMapper.Mapper.Map<IEnumerable<RestaurantModel>>(RestaurantList);

        //    return RestaurantModels;
        //}

        //public async Task<RestaurantModel> CreateRestaurant(RestaurantModel Restaurant)
        //{
        //    var existingRestaurant = await _restaurantRepository.GetByIdAsync(Restaurant.Id);
        //    if (existingRestaurant != null)
        //    {
        //        throw new ApplicationException("Restaurant with this id already exists");
        //    }

        //    var newRestaurant = ObjectMapper.Mapper.Map<Restaurant>(Restaurant);
        //    newRestaurant = await _restaurantRepository.SaveAsync(newRestaurant);

        //    _logger.LogInformation("Entity successfully added - MealmateAppService");

        //    var newRestaurantModel = ObjectMapper.Mapper.Map<RestaurantModel>(newRestaurant);
        //    return newRestaurantModel;
        //}

        //public async Task UpdateRestaurant(RestaurantModel Restaurant)
        //{
        //    var existingRestaurant = await _restaurantRepository.GetByIdAsync(Restaurant.Id);
        //    if (existingRestaurant == null)
        //    {
        //        throw new ApplicationException("Restaurant with this id is not exists");
        //    }

        //    existingRestaurant.Name = Restaurant.Name;
        //    existingRestaurant.Description = Restaurant.Description;

        //    await _restaurantRepository.SaveAsync(existingRestaurant);

        //    _logger.LogInformation("Entity successfully updated - MealmateAppService");
        //}

        //public async Task DeleteRestaurantById(int RestaurantId)
        //{
        //    var existingRestaurant = await _restaurantRepository.GetByIdAsync(RestaurantId);
        //    if (existingRestaurant == null)
        //    {
        //        throw new ApplicationException("Restaurant with this id is not exists");
        //    }

        //    await _restaurantRepository.DeleteAsync(existingRestaurant);

        //    _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        //}
    }
}
