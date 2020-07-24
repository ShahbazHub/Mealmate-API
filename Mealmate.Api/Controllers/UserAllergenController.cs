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
        [Route("{userId}/{isActive}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<UserAllergenModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserAllergenModel>>> Get(
            int userId, int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var UserAllergens = await _userAllergenService.Search(userId, isActive, request);
                JToken _jtoken = TokenService.CreateJToken(UserAllergens, request.Props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("single/{userAllergenId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(UserAllergenModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
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
        public async Task<ActionResult<UserAllergenModel>> Create(UserAllergenCreateModel request)
        {
            try
            {
                var result = await _userAllergenService.Create(request);
                return Created($"api/userallergens/{result.Id}", result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Update
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, UserAllergenUpdateModel request)
        {
            try
            {
                await _userAllergenService.Update(id, request);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{userAllergenId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int userAllergenId)
        {
            try
            {
                await _userAllergenService.Delete(userAllergenId);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

    }
}
