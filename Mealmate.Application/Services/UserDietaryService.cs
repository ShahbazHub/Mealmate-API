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
    public class UserDietaryService : IUserDietaryService
    {
        private readonly IUserDietaryRepository _UserDietaryRepository;
        private readonly IAppLogger<UserDietaryService> _logger;
        private readonly IMapper _mapper;

        public UserDietaryService(
            IUserDietaryRepository UserDietaryRepository,
            IAppLogger<UserDietaryService> logger,
            IMapper mapper)
        {
            _UserDietaryRepository = UserDietaryRepository ?? throw new ArgumentNullException(nameof(UserDietaryRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<UserDietaryModel> Create(UserDietaryCreateModel model)
        {
            var newUser = new UserDietary
            {
                Created = DateTime.Now,
                DietaryId = model.DietaryId,
                IsActive = model.IsActive
            };

            newUser = await _UserDietaryRepository.SaveAsync(newUser);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newUsermodel = _mapper.Map<UserDietaryModel>(newUser);
            return newUsermodel;
        }

        public async Task Delete(int id)
        {
            var existingUser = await _UserDietaryRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new ApplicationException("id is not exists");
            }

            await _UserDietaryRepository.DeleteAsync(existingUser);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<UserDietaryModel>> Get(int UserId)
        {
            var result = await _UserDietaryRepository.GetAsync(x => x.UserId == UserId);
            return _mapper.Map<IEnumerable<UserDietaryModel>>(result);
        }

        public async Task<UserDietaryModel> GetById(int id)
        {
            return _mapper.Map<UserDietaryModel>(await _UserDietaryRepository.GetByIdAsync(id));
        }

        public async Task Update(int id, UserDietaryUpdateModel model)
        {
            var existingUser = await _UserDietaryRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new ApplicationException("this id is not exists");
            }

            existingUser.IsActive = model.IsActive;

            await _UserDietaryRepository.SaveAsync(existingUser);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<UserDietaryModel>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _UserDietaryRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var DietaryModels = _mapper.Map<List<UserDietaryModel>>(TablePagedList.Items);

            var DietaryModelPagedList = new PagedList<UserDietaryModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                DietaryModels);

            return DietaryModelPagedList;
        }

        public async Task<IPagedList<UserDietaryModel>> Search(int userId, int isActive, PageSearchArgs args)
        {
            var TablePagedList = await _UserDietaryRepository.SearchAsync(userId, isActive, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var DietaryModels = _mapper.Map<List<UserDietaryModel>>(TablePagedList.Items);

            var DietaryModelPagedList = new PagedList<UserDietaryModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                DietaryModels);

            return DietaryModelPagedList;
        }
    }
}
