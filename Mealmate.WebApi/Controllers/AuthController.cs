using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mealmate.DataAccess.Entities.Identity;
using Mealmate.WebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Mealmate.WebApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IConfigurationRoot _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthController(
                    IConfigurationRoot config,
                    IMapper mapper,
                    UserManager<User> userManager,
                    SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Login
        [HttpPost("login")]
        public async Task<ActionResult> SignIn([FromBody] SignInModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Username);
                if (user == null)
                {
                    return NotFound($"Invalid username / Password");
                }

                var result = await _signInManager
                    .CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded)
                {
                    var appUser = await _userManager.Users
                                        .FirstOrDefaultAsync(
                        u => u.NormalizedUserName == model.Username.ToUpper());

                    var userToReturn = _mapper.Map<UserModel>(appUser);
                    return Ok(new
                    {
                        token = GenerateJwtToken(appUser).Result,
                        user = userToReturn,
                    });
                }

                return Unauthorized();
            }

            return BadRequest(ModelState);
        }
        #endregion

        #region Generate JWT
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
                .GetBytes(_config["AppSettings:Token"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,
                Issuer = _config["AppSettings:Issuer"],
                Audience = _config["AppSettings:Audience"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        #endregion

        #region Sign Up
        [HttpPost("signup")]
        public ActionResult SignUp()
        {
            return Ok();
        }
        #endregion

        #region Sign Out
        #endregion
    }
}