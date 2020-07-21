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
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IAppLogger<LocationService> _logger;
        private readonly IMapper _mapper;

        public LocationService(
            ILocationRepository locationRepository,
            IAppLogger<LocationService> logger,
            IMapper mapper)
        {
            _locationRepository = locationRepository ?? throw new ArgumentNullException(nameof(locationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<LocationModel> Create(LocationCreateModel model)
        {
            var newlocation = new Location
            {
                Name = model.Name,
                BranchId = model.BranchId,
                IsActive = model.IsActive,
                Created = DateTime.Now
            };

            newlocation = await _locationRepository.SaveAsync(newlocation);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newlocationmodel = _mapper.Map<LocationModel>(newlocation);
            return newlocationmodel;
        }

        public async Task Delete(int id)
        {
            var existingLocation = await _locationRepository.GetByIdAsync(id);
            if (existingLocation == null)
            {
                throw new ApplicationException("Location with this id is not exists");
            }

            existingLocation.IsActive = false;
            await _locationRepository.SaveAsync(existingLocation);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IEnumerable<LocationModel>> Get(int branchId)
        {
            var result = await _locationRepository.GetAsync(x => x.BranchId == branchId);
            return _mapper.Map<IEnumerable<LocationModel>>(result);
        }

        public async Task<LocationModel> GetById(int id)
        {
            return _mapper.Map<LocationModel>(await _locationRepository.GetByIdAsync(id));
        }

        public async Task Update(int id, LocationUpdateModel model)
        {
            var existingLocation = await _locationRepository.GetByIdAsync(id);
            if (existingLocation == null)
            {
                throw new ApplicationException($"Location with this id {id} does not exists");
            }

            existingLocation.Name = model.Name;
            existingLocation.IsActive = model.IsActive;

            await _locationRepository.SaveAsync(existingLocation);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<LocationModel>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _locationRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<LocationModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<LocationModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }

        public async Task<IPagedList<LocationModel>> Search(int branchId, int isActive, PageSearchArgs args)
        {
            var TablePagedList = await _locationRepository.SearchAsync(branchId, isActive, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<LocationModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<LocationModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }

        //public async Task<IEnumerable<LocationModel>> GetLocationList()
        //{
        //    var LocationList = await _locationRepository.ListAllAsync();

        //    var LocationModels = ObjectMapper.Mapper.Map<IEnumerable<LocationModel>>(LocationList);

        //    return LocationModels;
        //}

        //public async Task<IPagedList<LocationModel>> SearchLocations(PageSearchArgs args)
        //{
        //    var LocationPagedList = await _locationRepository.SearchLocationsAsync(args);

        //    //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
        //    var LocationModels = ObjectMapper.Mapper.Map<List<LocationModel>>(LocationPagedList.Items);

        //    var LocationModelPagedList = new PagedList<LocationModel>(
        //        LocationPagedList.PageIndex,
        //        LocationPagedList.PageSize,
        //        LocationPagedList.TotalCount,
        //        LocationPagedList.TotalPages,
        //        LocationModels);

        //    return LocationModelPagedList;
        //}

        //public async Task<LocationModel> GetLocationById(int LocationId)
        //{
        //    var Location = await _locationRepository.GetByIdAsync(LocationId);

        //    var LocationModel = ObjectMapper.Mapper.Map<LocationModel>(Location);

        //    return LocationModel;
        //}

        //public async Task<IEnumerable<LocationModel>> GetLocationsByName(string name)
        //{
        //    var spec = new LocationWithLocationesSpecification(name);
        //    var LocationList = await _locationRepository.GetAsync(spec);

        //    var LocationModels = ObjectMapper.Mapper.Map<IEnumerable<LocationModel>>(LocationList);

        //    return LocationModels;
        //}

        //public async Task<IEnumerable<LocationModel>> GetLocationsByCategoryId(int categoryId)
        //{
        //    var spec = new LocationWithLocationesSpecification(categoryId);
        //    var LocationList = await _locationRepository.GetAsync(spec);

        //    var LocationModels = ObjectMapper.Mapper.Map<IEnumerable<LocationModel>>(LocationList);

        //    return LocationModels;
        //}

        //public async Task<LocationModel> CreateLocation(LocationModel Location)
        //{
        //    var existingLocation = await _locationRepository.GetByIdAsync(Location.Id);
        //    if (existingLocation != null)
        //    {
        //        throw new ApplicationException("Location with this id already exists");
        //    }

        //    var newLocation = ObjectMapper.Mapper.Map<Location>(Location);
        //    newLocation = await _locationRepository.SaveAsync(newLocation);

        //    _logger.LogInformation("Entity successfully added - MealmateAppService");

        //    var newLocationModel = ObjectMapper.Mapper.Map<LocationModel>(newLocation);
        //    return newLocationModel;
        //}

        //public async Task UpdateLocation(LocationModel Location)
        //{
        //    var existingLocation = await _locationRepository.GetByIdAsync(Location.Id);
        //    if (existingLocation == null)
        //    {
        //        throw new ApplicationException("Location with this id is not exists");
        //    }

        //    existingLocation.Name = Location.Name;
        //    existingLocation.Description = Location.Description;

        //    await _locationRepository.SaveAsync(existingLocation);

        //    _logger.LogInformation("Entity successfully updated - MealmateAppService");
        //}

        //public async Task DeleteLocationById(int LocationId)
        //{
        //    var existingLocation = await _locationRepository.GetByIdAsync(LocationId);
        //    if (existingLocation == null)
        //    {
        //        throw new ApplicationException("Location with this id is not exists");
        //    }

        //    await _locationRepository.DeleteAsync(existingLocation);

        //    _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        //}
    }
}
