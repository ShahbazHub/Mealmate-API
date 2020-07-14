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
        /// <summary>
        /// Get Restaurant By OwnerId
        /// </summary>
        /// <param name="ownerId">Owner ID</param>
        /// <param name="props">Includes i.e Name, Branches(Name) </param>
        /// <returns></returns>
        [Route("{ownerId}")]
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

        [Route("{cuisineTypeIds}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RestaurantModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RestaurantModel>>> Get(List<int> cuisineTypeIds, string props)
        {
            try
            {
                var Restaurants = await _restaurantService.GetByCuisineTypes(cuisineTypeIds);
                JToken _jtoken = TokenService.CreateJToken(Restaurants, props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
        #endregion

        #region Create
        [Route("[action]")]
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
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(RestaurantModel request)
        {
            var result = await _restaurantService.Update(request);
            return Ok(result);
        }
        #endregion

        #region Delete
        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int restaurandId)
        {
            await _restaurantService.Delete(restaurandId);
            return Ok();
        }
        #endregion

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(IPagedList<RestaurantModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IPagedList<RestaurantModel>>> SearchRestaurants(SearchPageRequest request)
        //{
        //    var RestaurantPagedList = await _restaurantService.SearchRestaurants(request.Args);

        //    return Ok(RestaurantPagedList);
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(RestaurantModel), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<RestaurantModel>> GetRestaurantById(GetRestaurantByIdRequest request)
        //{
        //    var Restaurant = await _restaurantService.GetRestaurantById(request.Id);

        //    return Ok(Restaurant);
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(IEnumerable<RestaurantModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<RestaurantModel>>> GetRestaurantsByName(GetResturantsByNameRequest request)
        //{
        //    var Restaurants = await _restaurantService.GetRestaurantsByName(request.Name);

        //    return Ok(Restaurants);
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(IEnumerable<RestaurantModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<RestaurantModel>>> GetRestaurantsByCategoryId(GetRestaurantsByCategoryIdRequest request)
        //{
        //    var Restaurants = await _restaurantService.GetRestaurantsByCategoryId(request.CategoryId);

        //    return Ok(Restaurants);
        //}
    }
}
