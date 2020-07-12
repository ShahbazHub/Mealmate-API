using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Paging;


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<LocationModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<LocationModel>>> Get(int branchId)
        {
            var Locations = await _locationService.Get(branchId);

            return Ok(Locations);
        }
        #endregion

        #region Create
        [Route("[action]")]
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
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(LocationModel request)
        {
            await _locationService.Update(request);
            return Ok();
        }
        #endregion

        #region Delete
        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int locationId)
        {
            await _locationService.Delete(locationId);
            return Ok();
        }
        #endregion

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(IPagedList<LocationModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IPagedList<LocationModel>>> SearchLocations(SearchPageRequest request)
        //{
        //    var LocationPagedList = await _locationService.SearchLocations(request.Args);

        //    return Ok(LocationPagedList);
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(LocationModel), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<LocationModel>> GetLocationById(GetLocationByIdRequest request)
        //{
        //    var Location = await _locationService.GetLocationById(request.Id);

        //    return Ok(Location);
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(IEnumerable<LocationModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<LocationModel>>> GetLocationsByName(GetResturantsByNameRequest request)
        //{
        //    var Locations = await _locationService.GetLocationsByName(request.Name);

        //    return Ok(Locations);
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(IEnumerable<LocationModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<LocationModel>>> GetLocationsByCategoryId(GetLocationsByCategoryIdRequest request)
        //{
        //    var Locations = await _locationService.GetLocationsByCategoryId(request.CategoryId);

        //    return Ok(Locations);
        //}
    }
}
