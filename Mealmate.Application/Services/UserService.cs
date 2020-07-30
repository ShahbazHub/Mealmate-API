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
        //private readonly UserRestaurantService _userRestaurantService;
        //private readonly MealmateSettings _mealmateSettings;
        //private readonly ILogger _logger;

        public UserService(
            UserManager<User> userManager,
            IMapper mapper,
            IRestaurantService restaurantService,
            RoleManager<Role> roleManager
            //UserRestaurantService userRestaurantService,
            //IOptions<MealmateSettings> options
            //ILogger logger
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _restaurantService = restaurantService;
            _roleManager = roleManager;
            //_userRestaurantService = userRestaurantService;
            //_mealmateSettings = options.Value;
            //_logger = logger;
        }

        public async Task<UserModel> Create(UserModel model)
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
                bool roleExists = await _roleManager.RoleExistsAsync(model.Role);
                if (!roleExists)
                {
                    //Create Role
                    await _roleManager.CreateAsync(new Role(model.Role));
                }
                var userIsInRole = await _userManager.IsInRoleAsync(user, model.Role);
                if (!userIsInRole)
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                }

                var userRestaurant = new UserRestaurantCreateModel
                {
                    UserId = user.Id,
                    RestaurantId = model.RestaurantId,
                    IsActive = true,
                    IsOwner = false

                };

                //Todo: EmailService needs to be moved in application layer....

                //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //string siteURL = _mealmateSettings.ClientAppUrl;
                //var callbackUrl = string.Format("{0}/Account/ConfirmEmail?userId={1}&code={2}", siteURL, user.Id, token);
                ////var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                //var message = $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>";
                //await _emailService.SendEmailAsync(model.Email, "Confirm your account", message);


                userModel = _mapper.Map<UserModel>(user);
                //await _userRestaurantService.Create(userRestaurant);
                userModel.RestaurantId = userRestaurant.RestaurantId;
            }
            return userModel;
        }

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

        public async Task<IEnumerable<UserModel>> Get()
        {
            var result = _userManager.Users;
            var owners = _mapper.Map<IEnumerable<UserModel>>(result);

            foreach (var owner in owners)
            {
                var restaurants = await _restaurantService.Get(owner.Id);
                owner.Restaurants = restaurants;
            }

            return owners;
        }

        public async Task<UserModel> GetById(int id)
        {
            var result = await _userManager.Users.FirstOrDefaultAsync(p => p.Id == id);

            return _mapper.Map<UserModel>(result);
        }

        public async Task<UserModel> Update(UserModel model)
        {
            var existingUser = await _userManager.FindByIdAsync(model.Id.ToString());
            if (existingUser == null)
            {
                throw new ApplicationException("this id is not exists");
            }

            existingUser = _mapper.Map<User>(model);

            await _userManager.UpdateAsync(existingUser);
            //_logger.LogInformation("Entity successfully updated - MealmateAppService");

            return _mapper.Map<UserModel>(existingUser);
        }
    }
}
