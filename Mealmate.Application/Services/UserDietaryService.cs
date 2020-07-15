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

        public async Task<UserDietaryModel> Create(UserDietaryModel model)
        {
            var existingUser = await _UserDietaryRepository.GetByIdAsync(model.Id);
            if (existingUser != null)
            {
                throw new ApplicationException("User with this id already exists");
            }

            var newUser = _mapper.Map<UserDietary>(model);
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
                throw new ApplicationException("User with this id is not exists");
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

        public async Task Update(UserDietaryModel model)
        {
            var existingUser = await _UserDietaryRepository.GetByIdAsync(model.Id);
            if (existingUser == null)
            {
                throw new ApplicationException("User with this id is not exists");
            }

            existingUser = _mapper.Map<UserDietary>(model);

            await _UserDietaryRepository.SaveAsync(existingUser);

            _logger.LogInformation("Entity successfully updated - MealmateAppService");
        }


    }
}
