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
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/restaurants")]
    [ApiValidationFilter]
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
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<RestaurantModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RestaurantModel>>> Get([FromQuery] PageSearchArgs pageSearchArgs)
        {
            try
            {
                var Restaurants = await _restaurantService.Search(pageSearchArgs);
                JToken _jtoken = TokenService.CreateJToken(Restaurants, pageSearchArgs.Props);
                return Ok(new ApiOkResponse(new { _jtoken }));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
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
                return Ok(new ApiOkResponse(new { _jtoken }));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        [Route("{restaurantId}")]
        [HttpGet]
        [ProducesResponseType(typeof(RestaurantModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<RestaurantModel>> Get(int restaurantId)
        {
            try
            {
                var temp = await _restaurantService.GetById(restaurantId);
                if (temp == null)
                {
                    return NotFound(new ApiNotFoundResponse($"Resource with id {restaurantId} no more exists"));
                }
                 return Ok(new ApiOkResponse(new { temp }));
            }
            catch (Exception)
            {
                 return BadRequest(new ApiBadRequestResponse($"Error while processing request"));;
            }

        }
        #endregion

        #region Create
        [HttpPost]
        [ProducesResponseType(typeof(RestaurantModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<RestaurantModel>> Create(RestaurantWithOwnerCreateModel request)
        {
            try
            {
                var commandResult = await _restaurantService.Create(request.OwnerId,
                new RestaurantCreateModel { Name = request.Name, Description = request.Description, IsActive = request.IsActive });
                return Ok(commandResult);
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }
        #endregion

        #region Update
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, RestaurantUpdateModel request)
        {
            try
            {
                var result = await _restaurantService.Update(id, request);
                 return Ok(new ApiOkResponse(new { result }));;
            }
            catch (Exception )
            {
                 return BadRequest(new ApiBadRequestResponse($"Error while processing request"));;
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{restaurandId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int restaurandId)
        {
            await _restaurantService.Delete(restaurandId);
             return Ok(new ApiOkResponse());
        }
        #endregion
    }
}
