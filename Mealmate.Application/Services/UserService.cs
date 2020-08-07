using AutoMapper;

using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Configuration;
using Mealmate.Core.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mealmate.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IRestaurantService _restaurantService;
        private readonly IBranchService _branchService;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserRestaurantService _userRestaurantService;
        private readonly IUserBranchService _userBranchService;

        public UserService(
            UserManager<User> userManager,
            IMapper mapper,
            IRestaurantService restaurantService,
            IBranchService branchService,
            RoleManager<Role> roleManager,
            IUserRestaurantService userRestaurantService,
            IUserBranchService userBranchService
            )
        {
            _userBranchService = userBranchService;
            _branchService = branchService;
            _mapper = mapper;
            _userManager = userManager;
            _restaurantService = restaurantService;
            _roleManager = roleManager;
            _userRestaurantService = userRestaurantService;
        }

        #region Create
        public async Task<UserModel> Create(UserCreateModel model)
        {
            try
            {
                var userTemp = await _userManager.Users
                                .FirstOrDefaultAsync(p => p.Email.ToLower() == model.Email.ToLower());
                if (userTemp != null)
                {
                    throw new ApplicationException($"User with email {model.Email} already exists");
                }

                userTemp = await _userManager.Users
                               .FirstOrDefaultAsync(p => p.PhoneNumber == model.PhoneNumber);
                if (userTemp != null)
                {
                    throw new ApplicationException($"User with phone {model.PhoneNumber} already exists");
                }

                var restaurant = await _restaurantService.GetById(model.RestaurantId);
                if (restaurant == null)
                {
                    throw new ApplicationException("Restaurant does not exists");
                }

                var userModel = new UserModel();
                var user = new User
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    if (model.Roles.Count > 0)
                    {
                        foreach (var role in model.Roles)
                        {
                            await _userManager.AddToRoleAsync(user, role);
                        }
                    }

                    var userRestaurant = new UserRestaurantCreateModel
                    {
                        UserId = user.Id,
                        RestaurantId = model.RestaurantId,
                        IsActive = true,
                        IsOwner = false
                    };

                    if (model.Branches.Count > 0)
                    {
                        foreach (var item in model.Branches)
                        {
                            var branch = await _branchService.GetById(item);
                            if (branch != null)
                            {

                                var userBranch = new UserBranchCreateModel
                                {
                                    BranchId = item,
                                    IsActive = true,
                                    UserId = user.Id
                                };

                                var tempUserBranch = await _userBranchService.Create(userBranch);
                            }
                        }
                    }

                    var temp = await _userRestaurantService.Create(userRestaurant);

                    if (temp != null)
                    {
                        userModel = _mapper.Map<UserModel>(user);

                        var restaurants = await _restaurantService.Get(userModel.Id);
                        userModel.Restaurants = restaurants.ToList();

                        var branches = await _branchService.GetByEmployee(userModel.Id);
                        userModel.Branches = branches.ToList();

                        var roles = await _userManager.GetRolesAsync(_mapper.Map<User>(user));
                        foreach (var role in roles)
                        {
                            var tempRole = await _roleManager.FindByNameAsync(role);
                            userModel.Roles.Add(tempRole.Name);
                        }
                    }
                }
                return userModel;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{ex.Message}");
            }
        }
        #endregion

        #region Read
        public async Task<IEnumerable<UserModel>> Get()
        {
            var result = _userManager.Users;
            var users = _mapper.Map<List<UserModel>>(result);

            foreach (var user in users)
            {
                var restaurants = await _restaurantService.Get(user.Id);
                user.Restaurants = restaurants.ToList();

                var branches = await _branchService.GetByEmployee(user.Id);
                user.Branches = branches.ToList();

                var roles = await _userManager.GetRolesAsync(_mapper.Map<User>(user));
                foreach (var role in roles)
                {
                    var temp = await _roleManager.FindByNameAsync(role);
                    user.Roles.Add(temp.Name);
                }
            }

            return users;
        }

        public async Task<UserModel> GetById(int id)
        {
            var result = await _userManager.Users.FirstOrDefaultAsync(p => p.Id == id);

            var user = _mapper.Map<UserModel>(result);
            if (user != null)
            {
                var restaurants = await _restaurantService.Get(user.Id);
                user.Restaurants = restaurants.ToList();

                var branches = await _branchService.GetByEmployee(user.Id);
                user.Branches = branches.ToList();

                var roles = await _userManager.GetRolesAsync(_mapper.Map<User>(user));
                foreach (var role in roles)
                {
                    var temp = await _roleManager.FindByNameAsync(role);
                    user.Roles.Add(temp.Name);
                }
            }
            return user;
        }
        #endregion

        #region Update
        public async Task<UserModel> Update(int id, UserUpdateModel model)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    throw new ApplicationException("The user does not exists");
                }

                var userTemp = await _userManager.Users
                               .FirstOrDefaultAsync(p => p.Id != id && p.PhoneNumber== model.PhoneNumber);
                if (userTemp != null)
                {
                    throw new ApplicationException($"User with phone {model.PhoneNumber} already exists");
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.PhoneNumber = model.PhoneNumber;

                await _userManager.UpdateAsync(user);
                // _logger.LogInformation("Entity successfully updated - MealmateAppService");

                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }

                var userBranches = await _userBranchService.Get(id);
                foreach (var userBranch in userBranches)
                {
                    await _userBranchService.Delete(userBranch.Id);
                }

                if (model.Roles.Count > 0)
                {
                    foreach (var role in model.Roles)
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }
                }

                if (model.Branches.Count > 0)
                {
                    foreach (var item in model.Branches)
                    {
                        var branch = await _branchService.GetById(item);
                        if (branch != null)
                        {

                            var userBranch = new UserBranchCreateModel
                            {
                                BranchId = item,
                                IsActive = true,
                                UserId = user.Id
                            };

                            var tempUserBranch = await _userBranchService.Create(userBranch);
                        }
                    }
                }

                var userModel = _mapper.Map<UserModel>(user);

                var restaurants = await _restaurantService.Get(user.Id);
                userModel.Restaurants = restaurants.ToList();

                var branches = await _branchService.GetByEmployee(user.Id);
                userModel.Branches = branches.ToList();

                var rolesTemp = await _userManager.GetRolesAsync(_mapper.Map<User>(user));
                foreach (var role in rolesTemp)
                {
                    var temp = await _roleManager.FindByNameAsync(role);
                    userModel.Roles.Add(temp.Name);
                }

                return userModel;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{ex.Message}");
            }
        }
        #endregion

        #region Delete
        public async Task Delete(int id)
        {
            var existingUser = await _userManager.FindByIdAsync(id.ToString());
            if (existingUser == null)
            {
                throw new ApplicationException("User with this id is not exists");
            }

            await _userManager.DeleteAsync(existingUser);
            //_logger.LogInformation("Entity successfully deleted - MealmateAppService");
        }
        #endregion

    }
}
