using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Configuration;
using Mealmate.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mealmate.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MealmateSettings _mealmateSettings;
        private readonly IUserService _userService;

        public UserController(IUserService userService,
          IOptions<MealmateSettings> options)
        {
            _userService = userService;
            _mealmateSettings = options.Value;
        }


        #region Read
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            var result = await _userService.Get();
            return Ok(result);
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserModel>> GetById(int id)
        {
            var result = await _userService.GetById(id);
            if(result == null)
            {
                return NotFound($"User with id {id} no more exists");
            }
            return Ok(result);
        }
        #endregion

        #region Register
        #endregion

        #region Update
        #endregion

        #region Delete
        #endregion
    }
}
