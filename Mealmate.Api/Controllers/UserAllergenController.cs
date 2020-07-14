using Mealmate.Api.Helpers;
using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Paging;


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Mealmate.Api.Controllers
{
    [Route("api/userallergens")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserAllergenController : ControllerBase
    {
        private readonly IUserAllergenService _userAllergenService;

        public UserAllergenController(
            IUserAllergenService userAllergenService
            )
        {
            _userAllergenService = userAllergenService ?? throw new ArgumentNullException(nameof(userAllergenService));
        }

        #region Read
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(IEnumerable<UserAllergenModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserAllergenModel>>> Get(int userId, string props)
        {
            try
            {
                var UserAllergens = await _userAllergenService.Get(userId);
                JToken _jtoken = TokenService.CreateJToken(UserAllergens, props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{userAllergenId}")]
        [ProducesResponseType(typeof(UserAllergenModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserAllergenModel>> Get(int userAllergenId)
        {
            try
            {
                var temp = await _userAllergenService.Get(userAllergenId);
                return Ok(temp);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Create
        [HttpPost]
        [ProducesResponseType(typeof(UserAllergenModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<UserAllergenModel>> Create(UserAllergenModel request)
        {
            var result = await _userAllergenService.Create(request);
            return Created($"api/userallergens/{result.Id}", result);
        }
        #endregion

        #region Update
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(UserAllergenModel request)
        {
            await _userAllergenService.Update(request);
            return Ok();
        }
        #endregion

        #region Delete
        [HttpDelete("{userAllergenId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int userAllergenId)
        {
            await _userAllergenService.Delete(userAllergenId);
            return Ok();
        }
        #endregion

    }
}
