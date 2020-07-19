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
    [Route("api/restaurants")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(
            IRestaurantService restaurantService
            )
        {
            _restaurantService = restaurantService;
        }

        #region Read
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RestaurantModel>), (int)HttpStatusCode.OK)]
<<<<<<< HEAD
        public async Task<ActionResult<IEnumerable<RestaurantModel>>> Get(
            [FromBody] SearchPageRequest request, string props)
=======
        public async Task<ActionResult<IEnumerable<RestaurantModel>>> Get([FromQuery] PageSearchArgs pageSearchArgs)
>>>>>>> ccef4f60049b39d0b08fe59adf5af32533d3e908
        {
            try
            {
                var Restaurants = await _restaurantService.Search(pageSearchArgs);
                JToken _jtoken = TokenService.CreateJToken(Restaurants, pageSearchArgs.Props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Get Restaurant By OwnerId
        /// </summary>
        /// <param name="ownerId">Owner ID</param>
        /// <param name="props">Includes i.e Name, Branches(Name) </param>
        /// <returns></returns>
        [Route("list/{ownerId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RestaurantModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RestaurantModel>>> Get(int ownerId, string props)
        {
            try
            {
                var Restaurants = await _restaurantService.Get(ownerId);
                JToken _jtoken = TokenService.CreateJToken(Restaurants, props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("{restaurantId}")]
        [HttpGet]
        [ProducesResponseType(typeof(RestaurantModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RestaurantModel>> Get(int restaurantId)
        {
            try
            {
                var temp = await _restaurantService.Get(restaurantId);
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
        [ProducesResponseType(typeof(RestaurantModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<RestaurantModel>> Create(RestaurantModel request)
        {
            var commandResult = await _restaurantService.Create(request);

            return Ok(commandResult);
        }
        #endregion

        #region Update
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(RestaurantModel request)
        {
            var result = await _restaurantService.Update(request);
            return Ok(result);
        }
        #endregion

        #region Delete
        [HttpDelete("{restaurandId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int restaurandId)
        {
            await _restaurantService.Delete(restaurandId);
            return Ok();
        }
        #endregion
    }
}
