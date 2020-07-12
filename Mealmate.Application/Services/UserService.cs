using AutoMapper;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mealmate.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Task<UserModel> Create(UserModel model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserModel>> Get()
        {
            var result = _userManager.Users;

            return Task.FromResult(_mapper.Map<IEnumerable<UserModel>>(result));
        }

        public async Task<UserModel> GetById(int id)
        {
            var result = await _userManager.Users.FirstOrDefaultAsync(p => p.Id == id);

            return _mapper.Map<UserModel>(result);
        }

        public Task Update(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
