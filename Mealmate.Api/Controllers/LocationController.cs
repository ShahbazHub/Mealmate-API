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
    [Route("api/locations")]
    [ApiController]
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
        [Route("{branchId}")]
        [ProducesResponseType(typeof(IEnumerable<LocationModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<LocationModel>>> Get(
            int branchId, [FromBody] SearchPageRequest request, string props)
        {
            try
            {
                var Locations = await _locationService.Search(branchId, request.Args);
                JToken _jtoken = TokenService.CreateJToken(Locations, props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
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
                return Ok(Location);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Create
        [HttpPost]
        [ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<LocationModel>> Create(LocationModel request)
        {
            var result = await _locationService.Create(request);
            return Ok(result);
        }
        #endregion

        #region Update
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(LocationModel request)
        {
            await _locationService.Update(request);
            return Ok();
        }
        #endregion

        #region Delete
        [HttpDelete("{locationId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int locationId)
        {
            await _locationService.Delete(locationId);
            return Ok();
        }
        #endregion

    }
}
