using Mealmate.Api.Requests;
using Mealmate.Core.Configuration;
using Mealmate.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mealmate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly MealmateSettings _mealmateSettings;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AuthController(SignInManager<User> signInManager,
          UserManager<User> userManager,
          IOptions<MealmateSettings> options)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mealmateSettings = options.Value;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    // Create the token
                    var claims = new[]
                    {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        };

                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_mealmateSettings.Tokens.Key));
                    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                      _mealmateSettings.Tokens.Issuer,
                      _mealmateSettings.Tokens.Audience,
                      claims,
                      expires: DateTime.Now.AddMinutes(30),
                      signingCredentials: signingCredentials);

                    var results = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    };

                    return Created("", results);
                }
            }

            return Unauthorized();
        }
    }
}
