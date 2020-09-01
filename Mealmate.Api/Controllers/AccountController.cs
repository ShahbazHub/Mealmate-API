using AutoMapper;

using Mealmate.Api.Helpers;
using Mealmate.Api.Requests;
using Mealmate.Api.Services;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Configuration;
using Mealmate.Core.Entities;
using Mealmate.Infrastructure.Data;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Mealmate.Api.Controllers
{
    [ApiValidationFilter]
    [Route("api/accounts")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : ControllerBase
    {
        private readonly MealmateSettings _mealmateSettings;
        private readonly IRestaurantService _restaurantService;
        private readonly IBranchService _branchService;
        private readonly IUserRestaurantService _userRestaurantService;
        private readonly IEmailService _emailService;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserAllergenService _userAllergenService;
        private readonly IUserDietaryService _userDietaryService;
        private readonly IFacebookAuthService _facebookAuthService;
        private readonly IGoogleAuthService _googleAuthService;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IConfigurationRoot _config;
        private readonly IMapper _mapper;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly MealmateContext _mealmateContext;

        public AccountController(SignInManager<User> signInManager,
          UserManager<User> userManager,
          RoleManager<Role> roleManager,
          IOptions<MealmateSettings> options,
          IMapper mapper,
          IRestaurantService restaurantService,
          IBranchService branchService,
          IUserRestaurantService userRestaurantService,
          IEmailService emailService,
          IUserAllergenService userAllergenService,
          IUserDietaryService userDietaryService,
          IFacebookAuthService facebookAuthService,
          IGoogleAuthService googleAuthService,
          TokenValidationParameters tokenValidationParameters,
          MealmateContext mealmateContext,
          IConfigurationRoot config)
        {
            _config = config;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _mealmateSettings = options.Value;
            _restaurantService = restaurantService;
            _branchService = branchService;
            _userRestaurantService = userRestaurantService;
            _emailService = emailService;
            _roleManager = roleManager;
            _userAllergenService = userAllergenService;
            _userDietaryService = userDietaryService;
            _facebookAuthService = facebookAuthService;
            _googleAuthService = googleAuthService;
            _tokenValidationParameters = tokenValidationParameters;
            _mealmateContext = mealmateContext;
        }

        #region Create JWT
        private async Task<AuthenticationResult> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("id", user.Id.ToString())
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_mealmateSettings.Tokens.Key));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds,
                Issuer = _mealmateSettings.Tokens.Issuer,
                Audience = _mealmateSettings.Tokens.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddMonths(6)
            };

            await _mealmateContext.RefreshTokens.AddAsync(refreshToken);
            await _mealmateContext.SaveChangesAsync();

            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Id
            };

        }
        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
        {
            return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
                   jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase);
        }
        #endregion

        #region Get Principal 
        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Register
        /// <summary>
        /// his Register method can only be used by mealmate admin portal for registering new Reataurant Owners 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>A newly created User</returns>
        /// <response code="201">Returns the newly created User</response>
        /// <response code="400">If the item is null</response>      
        [AllowAnonymous]

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        public async Task<ActionResult> Register([FromBody] RegisterRequest model)
        {
            try
            {
                var user = new User
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email
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
                        var restaurant = new RestaurantCreateModel
                        {
                            Name = model.RestaurantName,
                            Description = model.RestaurantDescription,
                            IsActive = true
                        };

                        var data = await _restaurantService.Create(restaurant);

                        if (data != null)
                        {

                            var userRestaurant = new UserRestaurantCreateModel
                            {
                                UserId = user.Id,
                                RestaurantId = data.Id,
                                IsActive = true,
                                IsOwner = true

                            };

                            await _userRestaurantService.Create(userRestaurant);

                            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            try
                            {
                                string siteURL = _mealmateSettings.ClientAppUrl;
                                var callbackUrl = string.Format("{0}/verify?userid={1}&token={2}", siteURL, user.Id, token);
                                var message = $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>";
                                await _emailService.SendEmailAsync(model.Email, "Confirm your account", message);

                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }

                    var restaurants = await _restaurantService.Get(user.Id);

                    var owner = _mapper.Map<UserModel>(user);
                    owner.Restaurants = restaurants.ToList();

                    return Created($"/api/users/{user.Id}", new ApiCreatedResponse(owner));
                }
                return BadRequest(new ApiBadRequestResponse(result.Errors, "User already exists"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiBadRequestResponse($"{ex.Message}"));
            }
        }
        #endregion

        #region Register Mobile
        /// <summary>
        /// This Register method can only be used by andriod and ios for registering client users
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register-mobile")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        public async Task<ActionResult> MobileRegister([FromBody] MobileRegisterRequest model)
        {
            try
            {
                var user = new User
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };
                var userExists = await _userManager.FindByEmailAsync(model.Email);
                if (userExists != null)
                {
                    return BadRequest(new ApiBadRequestResponse("This email is already registered"));
                }
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    bool roleExists = await _roleManager.RoleExistsAsync(ApplicationRoles.Client);
                    if (!roleExists)
                    {
                        //Create Role
                        await _roleManager.CreateAsync(new Role(ApplicationRoles.Client));
                    }
                    var userIsInRole = await _userManager.IsInRoleAsync(user, ApplicationRoles.Client);
                    if (!userIsInRole)
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Client);
                    }

                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    string siteURL = _mealmateSettings.ClientAppUrl;
                    var callbackUrl = string.Format("{0}/Account/ConfirmEmail?userId={1}&code={2}", siteURL, user.Id, token);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    var message = $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>";
                    await _emailService.SendEmailAsync(model.Email, "Confirm your account", message);


                    if (model.UserAllergens.Count > 0)
                    {
                        foreach (var userAllergen in model.UserAllergens)
                        {
                            userAllergen.UserId = user.Id;
                            await _userAllergenService.Create(userAllergen);
                        }
                    }

                    if (model.UserDietaries.Count > 0)
                    {
                        foreach (var userDietary in model.UserDietaries)
                        {
                            userDietary.UserId = user.Id;
                            await _userDietaryService.Create(userDietary);
                        }
                    }
                    var owner = _mapper.Map<UserModel>(user);

                    return Created($"/api/users/{user.Id}", new ApiCreatedResponse(owner));
                }
                return BadRequest(new ApiBadRequestResponse(result.Errors, "Error registering new user"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiBadRequestResponse($"{ex.Message}"));
            }
        }
        #endregion

        #region Login
        /// <summary>
        /// signin using email and password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("web-signin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        public async Task<IActionResult> WebSignIn([FromBody] LoginRequest request)
        {
            //var userAgent = Request.Headers["User-Agent"];
            //string uaString = Convert.ToString(userAgent[0]);
            //var uaParser = Parser.GetDefault();
            //ClientInfo c = uaParser.Parse(uaString);

            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user != null)
                {
                    var restaurant = await _restaurantService.Get(user.Id);
                    if (restaurant.Count() == 0)
                    {
                        return Unauthorized(new ApiUnAuthorizedResponse("Only Restaurant Owner can login through this portal!"));
                    }
                    var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                    if (result.Succeeded)
                    {
                        var appUser = await _userManager.Users
                                                        .FirstOrDefaultAsync(
                                                        u => u.Email.ToUpper() == request.Email.ToUpper());

                        var userToReturn = _mapper.Map<UserModel>(appUser);
                        var authResponse = await GenerateJwtToken(appUser);

                        var restaurants = await _restaurantService.Get(appUser.Id);
                        userToReturn.Restaurants = restaurants.ToList();

                        var branches = await _branchService.GetByEmployee(appUser.Id);
                        userToReturn.Branches = branches.ToList();

                        //Saving Client FCM registration token
                        if (request.RegistrationToken != null)
                        {
                            _mealmateContext.FCMRegistrationTokens.Add(new FCMRegistrationToken
                            {
                                CreationDate = DateTime.UtcNow,
                                UserId = user.Id,
                                RegistrationToken = request.RegistrationToken,
                                ClientId = request.ClientId
                            });
                            _mealmateContext.SaveChanges();
                        }

                        return Ok(new ApiOkResponse(
                            new
                            {
                                token = authResponse.Token,
                                refreshToken = authResponse.RefreshToken,
                                user = userToReturn,
                            }));
                    }
                    else
                    {
                        return Unauthorized(new ApiUnAuthorizedResponse("Incorrect username / password"));
                    }
                }
                return Unauthorized(new ApiUnAuthorizedResponse("Incorrect username / password"));
            }
            catch (Exception ex)
            {
                return Unauthorized(new ApiBadRequestResponse("Error while processing request"));
            }
        }
        [AllowAnonymous]
        [HttpPost("signin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiBadRequestResponse))]
        public async Task<IActionResult> SignIn([FromBody] LoginRequest request)
        {
            //var userAgent = Request.Headers["User-Agent"];
            //string uaString = Convert.ToString(userAgent[0]);
            //var uaParser = Parser.GetDefault();
            //ClientInfo c = uaParser.Parse(uaString);

            try
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
                        var authResponse = await GenerateJwtToken(appUser);

                        var restaurants = await _restaurantService.Get(appUser.Id);
                        userToReturn.Restaurants = restaurants.ToList();

                        var branches = await _branchService.GetByEmployee(appUser.Id);
                        userToReturn.Branches = branches.ToList();
                       
                        //Saving Client FCM registration token
                        if (request.RegistrationToken != null)
                        {
                            _mealmateContext.FCMRegistrationTokens.Add(new FCMRegistrationToken
                            {
                                UserId = user.Id,
                                RegistrationToken = request.RegistrationToken,
                                ClientId = request.ClientId
                            });
                            _mealmateContext.SaveChanges();
                        }
                        return Ok(new ApiOkResponse(
                            new
                            {
                                token = authResponse.Token,
                                refreshToken = authResponse.RefreshToken,
                                user = userToReturn,
                            }));
                    }
                    else
                    {
                        return Unauthorized(new ApiUnAuthorizedResponse("Incorrect username / password"));
                    }
                }
                return Unauthorized(new ApiUnAuthorizedResponse("Incorrect username / password"));
            }
            catch (Exception)
            {
                return Unauthorized(new ApiBadRequestResponse("Error while processing request"));
            }
        }

        /// <summary>
        /// signin with facebook
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("signin-facebook")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnAuthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginWithFacebook([FromBody] FacebookLoginRequest request)
        {
            var validatedTokenResult = await _facebookAuthService.ValidateAccessTokenAsync(request.AccessToken);

            if (!validatedTokenResult.Data.IsValid)
            {
                return Unauthorized(new ApiUnAuthorizedResponse("your access token is invalid"));
            }

            var userInfo = await _facebookAuthService.GetUserInfoAsync(request.AccessToken);

            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var newUser = new User
                {
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName
                };

                var createdResult = await _userManager.CreateAsync(newUser);
                if (createdResult.Succeeded)
                {

                    var authResponse1 = await GenerateJwtToken(newUser);
                    var newUserToReturn = _mapper.Map<UserModel>(newUser);
                  
                    //Saving Client FCM registration token
                    if (request.RegistrationToken != null)
                    {
                        _mealmateContext.FCMRegistrationTokens.Add(new FCMRegistrationToken
                        {
                            UserId = user.Id,
                            RegistrationToken = request.RegistrationToken,
                            ClientId = request.ClientId
                        });
                        _mealmateContext.SaveChanges();
                    }
                    return Created($"/api/users/{newUser.Id}", new ApiCreatedResponse(new
                    {
                        token = authResponse1.Token,
                        refreshToken = authResponse1.RefreshToken,
                        user = newUserToReturn
                    }));
                }

                return BadRequest(new ApiBadRequestResponse(createdResult.Errors, "test"));
            }
            var authResponse = await GenerateJwtToken(user);
            var userToReturn = _mapper.Map<UserModel>(user);
            return Ok(new ApiOkResponse(new
            {
                token = authResponse.Token,
                refreshToken = authResponse.RefreshToken,
                user = userToReturn
            }));
        }

        /// <summary>
        /// signin with google
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("signin-google")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnAuthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> LoginWithGoogle([FromBody] GoogleLoginRequest request)
        {
            //var validatedTokenResult = await _googleAuthService.ValidateAccessTokenAsync(request.AccessToken);

            //if (validatedTokenResult == null)
            //{
            //    return BadRequest("your access token is invalid");
            //}

            var userInfo = await _googleAuthService.GetUserInfoAsync(request.IdToken);
            if (userInfo == null)
            {
                return Unauthorized(new ApiUnAuthorizedResponse("your idtoken is invalid"));
            }

            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var newUser = new User
                {
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    FirstName = userInfo.Name,
                    LastName = userInfo.FamilyName
                };

                var createdResult = await _userManager.CreateAsync(newUser);
                if (!createdResult.Succeeded)
                {
                    return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
                }
                var authResponse1 = await GenerateJwtToken(newUser);
                var newUserToReturn = _mapper.Map<UserModel>(newUser);

                //Saving Client FCM registration token
                if (request.RegistrationToken != null)
                {
                    _mealmateContext.FCMRegistrationTokens.Add(new FCMRegistrationToken
                    {
                        UserId = user.Id,
                        RegistrationToken = request.RegistrationToken,
                        ClientId = request.ClientId
                    });
                    _mealmateContext.SaveChanges();
                }
                return Created($"/api/users/{newUser.Id}", new ApiCreatedResponse(new
                {
                    token = authResponse1.Token,
                    refreshToken = authResponse1.RefreshToken,
                    user = newUserToReturn
                }));

            }
            var authResponse = await GenerateJwtToken(user);
            var userToReturn = _mapper.Map<UserModel>(user);
            return Ok(new ApiOkResponse(new
            {
                token = authResponse.Token,
                refreshToken = authResponse.RefreshToken,
                user = userToReturn
            }));
        }

        #endregion

        #region Sign Out
        /// <summary>
        /// User Signout will signout the user on server side.
        /// </summary>
        /// <param name="signOutModel"></param>
        /// <returns></returns>
        [HttpPost("signout")]
        public async Task<IActionResult> SignOut([FromBody] SignOutModel signOutModel)
        {
            var user = await _userManager.FindByEmailAsync(signOutModel.Email);
            if (user != null)
            {
                await _signInManager.SignOutAsync();

                //Removing user FCM registration token for device signing out

                if (signOutModel.ClientId != null)
                {
                    var fcmToken = _mealmateContext.FCMRegistrationTokens.First(x => x.ClientId == signOutModel.ClientId);
                    if (fcmToken != null)
                    {
                        _mealmateContext.FCMRegistrationTokens.Remove(fcmToken);
                        _mealmateContext.SaveChanges();
                    }
                }
                return Ok(new ApiOkResponse("SignOut Successfull"));
            }
            return Unauthorized(new ApiUnAuthorizedResponse("SignOut Failed"));
        }
        #endregion

        #region Refresh Token
        /// <summary>
        /// To get new token if token has expired by providing Expired token and refresh token
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            var validatedToken = GetPrincipalFromToken(request.ExpiredToken);

            if (validatedToken == null)
            {
                return Unauthorized(new ApiUnAuthorizedResponse("Invalid Token"));
            }

            var expiryDateUnix =
                long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                return BadRequest(new ApiBadRequestResponse("This token hasn't expired yet"));
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            var storedRefreshToken = await _mealmateContext.RefreshTokens.SingleOrDefaultAsync(x => x.Id == request.RefreshToken);

            if (storedRefreshToken == null)
            {
                return BadRequest(new ApiBadRequestResponse("This refresh token does not exist"));
            }

            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                return BadRequest(new ApiBadRequestResponse("This refresh token has expired"));
            }

            if (storedRefreshToken.Invalidated)
            {
                return BadRequest(new ApiBadRequestResponse("This refresh token has been invalidated"));
            }

            if (storedRefreshToken.Used)
            {
                return BadRequest(new ApiBadRequestResponse("This refresh token has been used"));
            }

            if (storedRefreshToken.JwtId != jti)
            {
                return BadRequest(new ApiBadRequestResponse("This refresh token does not match this JWT"));
            }

            storedRefreshToken.Used = true;
            _mealmateContext.RefreshTokens.Update(storedRefreshToken);
            await _mealmateContext.SaveChangesAsync();

            var user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);
            var authResponse = await GenerateJwtToken(user);

            return Ok(new ApiOkResponse(new
            {
                token = authResponse.Token,
                refreshToken = authResponse.RefreshToken
            }));
        }
        #endregion

        #region Change Password
        //[HttpPost("changePassword")]
        //public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        //{
        //    var user = await _userManager.FindByEmailAsync(request.Email);
        //    var result = await _signInManager.CheckPasswordSignInAsync(user, request.OldPassword, false);
        //    if (result.Succeeded)
        //    {
        //        var change = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
        //        if (change.Succeeded)
        //             return Ok(new ApiOkResponse());
        //    }

        //    return Unauthorized();
        //}
        #endregion

        #region Reset Password
        //[AllowAnonymous]
        //[HttpGet("resetpassword")]
        //public async Task<ActionResult> ResetPassword(string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);
        //    if (user != null)
        //    {
        //        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        //        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, token }, protocol: HttpContext.Request.Scheme);
        //        var message = $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>";
        //        await _emailService.SendEmailAsync(email, "Reset Password", message);
        //        return Ok("Check Your email...");
        //    }
        //    return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
        //}

        //[AllowAnonymous]
        //[HttpPost("resetpassword")]
        //public async Task<ActionResult> ChangePassword([FromBody] ResetPasswordRequest request)
        //{
        //    var user = await _userManager.FindByNameAsync(request.UserName);
        //    if (user != null)
        //    {
        //        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
        //        if (result.Succeeded)
        //        {
        //            return Ok("User Password Changed Successfully!");
        //        }
        //    }
        //    return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
        //}

        #endregion

        #region Forgot Password
        /// <summary>
        /// Generates and OPT to Reset the password and sends to the user register mobile number
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        [HttpPost()]
        [Route("forgot-password")]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return NotFound(new ApiNotFoundResponse($"User with email {model.Email} no more exists"));
                }

                var userOtps = _mealmateContext.UserOtps.Where(p => p.UserId == user.Id && p.IsActive == true);
                foreach (var item in userOtps)
                {
                    item.IsActive = false;
                    _mealmateContext.UserOtps.Update(item);
                    await _mealmateContext.SaveChangesAsync();
                }
                var otp = new Random().Next(100000, 999999);

                var userOtp = new UserOtp
                {
                    UserId = user.Id,
                    Otp = otp.ToString(),
                    StartTime = DateTime.UtcNow.TimeOfDay,
                    EndTime = DateTime.UtcNow.AddMinutes(10).TimeOfDay,
                    IsActive = true
                };

                await _mealmateContext.UserOtps.AddAsync(userOtp);
                if (await _mealmateContext.SaveChangesAsync() > 0)
                {
                    var accountId = _config["Twilio:AccountId"];
                    var token = _config["Twilio:Token"];

                    TwilioClient.Init(accountId, token);

                    var message = MessageResource.Create(
                        body: $"Your OTP is {otp}",
                        from: new Twilio.Types.PhoneNumber(_config["Twilio:PhoneNumber"]),
                        to: new Twilio.Types.PhoneNumber(user.PhoneNumber)
                    );

                    return Ok(new ApiOkResponse(message.Sid));
                }
                return BadRequest(new ApiBadRequestResponse("Error While Generating OTP"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiBadRequestResponse(ex.Message));
            }
        }
        #endregion

        #region Reset Password Using OTP
        /// <summary>
        /// Reset Password using OPT
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost()]
        [Route("reset-password")]
        public async Task<ActionResult> ResetPasswordUsingOTP([FromBody] ChangePasswordModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return NotFound(new ApiNotFoundResponse($"User with email {model.Email} no more exists"));
                }

                var nowTime = DateTime.UtcNow.TimeOfDay;

                var userOTP = await _mealmateContext.UserOtps.FirstOrDefaultAsync(p => p.UserId == user.Id
                                    && p.Otp == model.OPT
                                    && p.IsActive == true
                                    && (p.StartTime.CompareTo(nowTime) < 0 && p.EndTime.CompareTo(nowTime) > 0));

                if (userOTP != null && userOTP.Otp == model.OPT)
                {
                    var result = await _userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await _userManager.AddPasswordAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            userOTP.IsActive = false;
                            _mealmateContext.UserOtps.Update(userOTP);
                            await _mealmateContext.SaveChangesAsync();
                            return Ok(new ApiOkResponse("Password changed successfully"));
                        }
                    }
                    return BadRequest(new ApiBadRequestResponse("Error while processing your request"));
                }
                else
                {
                    return NotFound(new ApiNotFoundResponse($"OTP not matched / expired"));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiBadRequestResponse(ex.Message));
            }

        }
        #endregion

        #region Verify OTP
        //[HttpPost()]
        //[Route("verifyOTP")]
        //public async Task<ActionResult> VerifyOTP([FromBody] OTPVerifyModel model)
        //{
        //    try
        //    {
        //        var user = await _userManager.FindByEmailAsync(model.Email);
        //        if (user == null)
        //        {
        //            return NotFound(new ApiNotFoundResponse($"User with email {model.Email} no more exists");
        //        }

        //        var nowTime = DateTime.UtcNow.TimeOfDay;

        //        var userOtp = await _mealmateContext
        //                            .UserOtps
        //                            .FirstOrDefaultAsync(p => p.UserId == user.Id
        //                            && p.Otp == model.Otp && p.IsActive == true
        //                            && (p.StartTime.CompareTo(nowTime) < 0 && p.EndTime.CompareTo(nowTime) > 0));

        //        if (userOtp == null)
        //        {
        //            return NotFound(new ApiNotFoundResponse($"OTP not matched / expired");
        //        }

        //        userOtp.IsActive = false;
        //        _mealmateContext.UserOtps.Update(userOtp);
        //        await _mealmateContext.SaveChangesAsync();

        //        return Ok("OTP matched");
        //    }
        //    catch (Exception)
        //    {
        //        //TODO: Log errors
        //    }

        //     return BadRequest(new ApiBadRequestResponse($"Error while processing request"));;
        //}
        #endregion
        [AllowAnonymous]
        [HttpPost()]
        [Route("verify-email")]
        public IActionResult ConfirmEmail([FromBody] ConfirmEmailModel request)
        {
            User user = _userManager.FindByIdAsync(request.UserId).Result;
            IdentityResult result = _userManager.ConfirmEmailAsync(user, request.Token).Result;
            if (result.Succeeded)
            {
                return Ok(new ApiOkResponse("Email confirmed successfully!"));
            }
            return BadRequest();
        }
    }
}
