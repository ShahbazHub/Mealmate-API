using AutoMapper;

using Mealmate.Api.Helpers;
using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Configuration;
using Mealmate.Core.Entities;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Mealmate.Api.Controllers
{
    /// <summary>
    /// Account Controller
    /// </summary>
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MealmateSettings _mealmateSettings;
        private readonly IRestaurantService _restaurantService;
        private readonly IEmailService _emailService;
        private RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AccountController(SignInManager<User> signInManager,
          UserManager<User> userManager,
          RoleManager<Role> roleManager,
          IOptions<MealmateSettings> options,
          IMapper mapper,
          IRestaurantService restaurantService,
          IEmailService emailService)
        {
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _mealmateSettings = options.Value;
            _restaurantService = restaurantService;
            _emailService = emailService;
            _roleManager = roleManager;
        }

        #region Create JWT
        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_mealmateSettings.Tokens.Key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
                Issuer = _mealmateSettings.Tokens.Issuer,
                Audience = _mealmateSettings.Tokens.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        #endregion

        #region Login
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    var appUser = await _userManager.Users
                                                    .FirstOrDefaultAsync(
                                           u => u.Email.ToUpper() == request.Email.ToUpper());

                    var userToReturn = _mapper.Map<UserModel>(appUser);
                    return Ok(new
                    {
                        token = GenerateJwtToken(appUser).Result,
                        user = userToReturn,
                    });
                }
                else
                {
                    return Unauthorized("UserName of Password is incorrect");
                }

            }
            return Unauthorized("UserName of Password is incorrect");

        }
        #endregion

        #region Change Password
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.OldPassword, false);
            if (result.Succeeded)
            {
                var change = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
                if (change.Succeeded)
                    return Ok();
            }

            return Unauthorized();
        }
        #endregion

        #region Sign Out
        [HttpPost("logout")]
        public async Task<IActionResult> SignOut(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                return Ok();
            }

            return Unauthorized();
        }
        #endregion

        #region Register
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest model)
        {
            //TODO: Add you code here

            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    bool roleExists = await _roleManager.RoleExistsAsync(ApplicationRoles.RestaurantAdmin);
                    if (!roleExists)
                    {
                        //Create Role
                        await _roleManager.CreateAsync(new Role(ApplicationRoles.RestaurantAdmin));
                    }
                    var userIsInRole = await _userManager.IsInRoleAsync(user, ApplicationRoles.RestaurantAdmin);
                    if (!userIsInRole)
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.RestaurantAdmin);
                    }

                    if (model.IsRestaurantAdmin)
                    {
                        await _restaurantService.Create(new RestaurantModel
                        {
                            OwnerId = user.Id,
                            Name = model.RestaurantName,
                            Description = model.RestaurantDescription
                        });

                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        
                        string siteURL = _mealmateSettings.ClientAppUrl;
                        var callbackUrl = string.Format("{0}/Account/ConfirmEmail?userId={1}&code={2}", siteURL, user.Id, token);
                        //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                        var message = $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>";
                        await _emailService.SendEmailAsync(model.Email, "Confirm your account", message);
                    }

                    var restaurants = await _restaurantService.Get(user.Id);

                    var owner = _mapper.Map<UserModel>(user);
                    owner.Restaurants = restaurants;

                    return Created($"/api/users/{user.Id}", owner);
                }
                else
                {
                    return BadRequest($"Error registering new user");
                }
            }
            return BadRequest(ModelState);
        }
        #endregion

        #region ResetPassword
        [AllowAnonymous]
        [HttpGet("resetpassword")]
        public async Task<ActionResult> ResetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, protocol: HttpContext.Request.Scheme);
                var message = $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>";
                await _emailService.SendEmailAsync(email, "Reset Password", message);
                return Ok("Check Your email...");
            }
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("resetpassword")]
        public async Task<ActionResult> ChangePassword([FromBody] ResetPasswordRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
                if (result.Succeeded)
                {
                    return Ok("User Password Changed Successfully!");
                }
            }
            return BadRequest();
        }

        #endregion
    }
}
