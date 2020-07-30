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
    public class UserAllergenService : IUserAllergenService
    {
        private readonly IUserAllergenRepository _UserAllergenRepository;
        private readonly IAppLogger<UserAllergenService> _logger;
        private readonly IMapper _mapper;

        public UserAllergenService(
            IUserAllergenRepository UserAllergenRepository,
            IAppLogger<UserAllergenService> logger,
            IMapper mapper)
        {
            _UserAllergenRepository = UserAllergenRepository ?? throw new ArgumentNullException(nameof(UserAllergenRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper;
        }

        public async Task<UserAllergenModel> Create(UserAllergenCreateModel model)
        {
            var newUser = new UserAllergen
            {
                AllergenId = model.AllergenId,
                Created = DateTime.Now,
                UserId = model.UserId,
                IsActive = model.IsActive
            };

            newUser = await _UserAllergenRepository.SaveAsync(newUser);

            _logger.LogInformation("entity successfully added - mealmateappservice");

            var newUsermodel = _mapper.Map<UserAllergenModel>(newUser);
            return newUsermodel;
        }

        public async Task Delete(int id)
        {
            var existingUser = await _UserAllergenRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new ApplicationException("User with this id is not exists");
            }

            await _UserAllergenRepository.DeleteAsync(existingUser);

            _logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }

        public async Task<IEnumerable<UserAllergenModel>> Get(int UserId)
        {
            var result = await _UserAllergenRepository.GetAsync(x => x.UserId == UserId);
            return _mapper.Map<IEnumerable<UserAllergenModel>>(result);
        }

        public async Task<UserAllergenModel> GetById(int id)
        {
            return _mapper.Map<UserAllergenModel>(await _UserAllergenRepository.GetByIdAsync(id));
        }

        public async Task Update(int id, UserAllergenUpdateModel model)
        {
            var existingUser = await _UserAllergenRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new ApplicationException("User with this id is not exists");
            }

            existingUser.IsActive = model.IsActive;

            await _UserAllergenRepository.SaveAsync(existingUser);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }

        public async Task<IPagedList<UserAllergenModel>> Search(PageSearchArgs args)
        {
            var TablePagedList = await _UserAllergenRepository.SearchAsync(args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<UserAllergenModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<UserAllergenModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }

        public async Task<IPagedList<UserAllergenModel>> Search(int userId, int isActive, PageSearchArgs args)
        {
            var TablePagedList = await _UserAllergenRepository.SearchAsync(userId, isActive, args);

            //TODO: PagedList<TSource> will be mapped to PagedList<TDestination>;
            var AllergenModels = _mapper.Map<List<UserAllergenModel>>(TablePagedList.Items);

            var AllergenModelPagedList = new PagedList<UserAllergenModel>(
                TablePagedList.PageIndex,
                TablePagedList.PageSize,
                TablePagedList.TotalCount,
                TablePagedList.TotalPages,
                AllergenModels);

            return AllergenModelPagedList;
        }

    }
}
