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
    [Route("api/users")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MealmateSettings _mealmateSettings;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager,
          UserManager<User> userManager,
          IOptions<MealmateSettings> options)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mealmateSettings = options.Value;
        }

    }
}
