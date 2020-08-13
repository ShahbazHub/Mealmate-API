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
    [Route("api/locations")]
    [ApiValidationFilter]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(
            ILocationService locationService
            )
        {
            _locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
        }

        #region Read
        [HttpGet]
        [Route("{branchId}/{isActive}")]
        [ProducesResponseType(typeof(IEnumerable<LocationModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<LocationModel>>> Get(
            int branchId, int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var Locations = await _locationService.Search(branchId, isActive, request);
                JToken _jtoken = TokenService.CreateJToken(Locations, request.Props);
                return Ok(new ApiOkResponse(new { _jtoken }));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }
        [HttpGet]
        [Route("single/{locationId}")]
        [ProducesResponseType(typeof(IEnumerable<LocationModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<LocationModel>>> Get(int locationId)
        {
            try
            {
                var Location = await _locationService.GetById(locationId);
                if (Location == null)
                {
                    return NotFound(new ApiNotFoundResponse($"Resource with id {locationId} no more exists"));
                }
                return Ok(Location);
            }
            catch (Exception)
            {
                return BadRequest("Error while processing request");
            }
        }
        #endregion

        #region Create
        [HttpPost]
        [ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<LocationModel>> Create(LocationCreateModel request)
        {
            try
            {
                var result = await _locationService.Create(request);
                 return Ok(new ApiOkResponse(new { result }));;

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
        public async Task<ActionResult> Update(int id, LocationUpdateModel request)
        {
            try
            {
                await _locationService.Update(id, request);
                 return Ok(new ApiOkResponse());
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }

        }
        #endregion

        #region Delete
        [HttpDelete("{locationId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int locationId)
        {
            try
            {
                await _locationService.Delete(locationId);
                 return Ok(new ApiOkResponse());
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }
        #endregion

    }
}
