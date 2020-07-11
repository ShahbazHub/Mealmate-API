using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Paging;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IMediator _mediator;
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(
            IMediator mediator,
            IRestaurantService restaurantService
            )
        {
            _mediator = mediator;
            _restaurantService = restaurantService;
        }

        #region Read
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RestaurantModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RestaurantModel>>> Get(int ownerId)
        {
            var Restaurants = await _restaurantService.Get(ownerId);

            return Ok(Restaurants);
        }
        #endregion

        #region Create
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(RestaurantModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<RestaurantModel>> Create(CreateRequest<RestaurantModel> request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
        }
        #endregion

        #region Update
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(UpdateRequest<RestaurantModel> request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
        }
        #endregion

        #region Delete
        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(DeleteByIdRequest request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
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
