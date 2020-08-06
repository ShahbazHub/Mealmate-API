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
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserRestaurantService _userRestaurantService;

        public UserService(
            UserManager<User> userManager,
            IMapper mapper,
            IRestaurantService restaurantService,
            RoleManager<Role> roleManager,
            IUserRestaurantService userRestaurantService
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _restaurantService = restaurantService;
            _roleManager = roleManager;
            _userRestaurantService = userRestaurantService;
        }

        #region Create
        public async Task<UserModel> Create(UserCreateModel model)
        {
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
                        await _userManager.AddToRoleAsync(user, role.Name);
                    }
                }

                var userRestaurant = new UserRestaurantCreateModel
                {
                    UserId = user.Id,
                    RestaurantId = model.RestaurantId,
                    IsActive = true,
                    IsOwner = false
                };

                var temp = await _userRestaurantService.Create(userRestaurant);
                if (temp != null)
                {
                    userModel = _mapper.Map<UserModel>(user);
                    userModel.RestaurantId = userRestaurant.RestaurantId;

                    var restaurants = await _restaurantService.Get(userModel.Id);
                    userModel.Restaurants = restaurants.ToList();

                    var roles = await _userManager.GetRolesAsync(_mapper.Map<User>(user));
                    foreach (var role in roles)
                    {
                        var tempRole = await _roleManager.FindByNameAsync(role);
                        userModel.Roles.Add(new UserRoleModel
                        {
                            Name = tempRole.Name,
                            RoleId = tempRole.Id
                        });
                    }
                }
            }
            return userModel;
        }
        #endregion

        #region Read
        public async Task<IEnumerable<UserModel>> Get()
        {
            var result = _userManager.Users;
            var users = _mapper.Map<IEnumerable<UserModel>>(result);

            foreach (var user in users)
            {
                var restaurants = await _restaurantService.Get(user.Id);
                user.Restaurants = restaurants.ToList();

                var roles = await _userManager.GetRolesAsync(_mapper.Map<User>(user));
                foreach (var role in roles)
                {
                    var temp = await _roleManager.FindByNameAsync(role);
                    user.Roles.Add(new UserRoleModel
                    {
                        Name = temp.Name,
                        RoleId = temp.Id
                    });
                }
            }

            return users;
        }

        public async Task<UserModel> GetById(int id)
        {
            var result = await _userManager.Users.FirstOrDefaultAsync(p => p.Id == id);

            var user = _mapper.Map<UserModel>(result);

            var restaurants = await _restaurantService.Get(user.Id);
            user.Restaurants = restaurants.ToList();

            var roles = await _userManager.GetRolesAsync(_mapper.Map<User>(user));
            foreach (var role in roles)
            {
                var temp = await _roleManager.FindByNameAsync(role);
                user.Roles.Add(new UserRoleModel
                {
                    Name = temp.Name,
                    RoleId = temp.Id
                });
            }

            return user;
        }
        #endregion

        #region Update
        public async Task<UserModel> Update(int id, UserUpdateModel model)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                throw new ApplicationException("this id is not exists");
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            await _userManager.UpdateAsync(user);
            // _logger.LogInformation("Entity successfully updated - MealmateAppService");

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                await _userManager.RemoveFromRoleAsync(user, role);
            }

            if (model.Roles.Count > 0)
            {
                foreach (var role in model.Roles)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
            }

            var userModel = _mapper.Map<UserModel>(user);

            var restaurants = await _restaurantService.Get(user.Id);
            userModel.Restaurants = restaurants.ToList();

            var rolesTemp = await _userManager.GetRolesAsync(_mapper.Map<User>(user));
            foreach (var role in rolesTemp)
            {
                var temp = await _roleManager.FindByNameAsync(role);
                userModel.Roles.Add(new UserRoleModel
                {
                    Name = temp.Name,
                    RoleId = temp.Id
                });
            }

            return userModel;
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
