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
    [Route("api/userdietaries")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserDietaryController : ControllerBase
    {
        private readonly IUserDietaryService _userDietaryService;

        public UserDietaryController(
            IUserDietaryService userDietaryService
            )
        {
            _userDietaryService = userDietaryService ?? throw new ArgumentNullException(nameof(userDietaryService));
        }

        #region Read
        [Route("{userId}/{isActive}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<UserDietaryModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserDietaryModel>>> Get(
            int userId, int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var UserDietarys = await _userDietaryService.Search(userId, isActive, request);
                JToken _jtoken = TokenService.CreateJToken(UserDietarys, request.Props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("single/{userDietaryId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(UserDietaryModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserDietaryModel>> Get(int userDietaryId)
        {
            try
            {
                var temp = await _userDietaryService.Get(userDietaryId);
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
        [ProducesResponseType(typeof(UserDietaryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<UserDietaryModel>> Create(UserDietaryCreateModel request)
        {
            try
            {
                var result = await _userDietaryService.Create(request);
                return Created($"api/userallergens/{result.Id}", result);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Update
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, UserDietaryUpdateModel request)
        {
            try
            {
                await _userDietaryService.Update(id, request);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{userDietaryId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int userDietaryId)
        {
            try
            {
                await _userDietaryService.Delete(userDietaryId);
                return Ok();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
        }
        #endregion

    }
}
