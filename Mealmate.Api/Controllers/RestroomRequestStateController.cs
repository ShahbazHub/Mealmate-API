using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Mealmate.Api.Helpers;
using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities.Lookup;
using Mealmate.Core.Paging;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Mealmate.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/restroomrequeststates")]
    [ApiValidationFilter]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RestroomRequestStateController : ControllerBase
    {
        private readonly IRestroomRequestStateService _restroomrequestStateService;

        public RestroomRequestStateController(IRestroomRequestStateService restroomrequestStateService)
        {
            _restroomrequestStateService = restroomrequestStateService ?? throw new ArgumentNullException(nameof(restroomrequestStateService));
        }

        #region Read
        /// <summary>
        /// List all Restroom Request States
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<RestroomRequestStateModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RestroomRequestStateModel>>> Get()
        {
            try
            {
                var result = await _restroomrequestStateService.Get();
                if (result != null)
                {
                    result = result.Where(p => p.IsActive == true);
                }
                 return Ok(new ApiOkResponse(new { result }));;
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        /// <summary>
        /// List all Restroom Request States with specific state (0: Passive, 1: Active, 2: All)
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("list/{isActive}")]
        [ProducesResponseType(typeof(IEnumerable<RestroomRequestStateModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RestroomRequestStateModel>>> Get(
            int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var result = await _restroomrequestStateService.Search(isActive, request);
                JToken _jtoken = TokenService.CreateJToken(result, request.Props);
                return Ok(new ApiOkResponse(new { _jtoken }));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        /// <summary>
        /// Get a specific Restroom Request State
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<RestroomRequestStateModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RestroomRequestStateModel>>> Get(int id)
        {
            try
            {
                var temp = await _restroomrequestStateService.GetById(id);
                if (temp == null)
                {
                    return NotFound(new ApiNotFoundResponse($"Resource with id {id} no more exists"));
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
        /// <summary>
        /// Create a new Restroom Request State
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] RestroomRequestStateCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _restroomrequestStateService.Create(model);
                if (result != null)
                {
                    return Created($"api/restroomrequestStates/{result.Id}", result);
                }
            }

             return BadRequest(new ApiBadRequestResponse(ModelState, $"Error while processing request"));;
        }
        #endregion

        #region Update
        /// <summary>
        /// Update an existing Restroom Request State
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, RestroomRequestStateUpdateModel model)
        {
            //TODO: Add you code here
            try
            {
                if (ModelState.IsValid)
                {
                    await _restroomrequestStateService.Update(id, model);
                }
            }
            catch (Exception )
            {
                 return BadRequest(new ApiBadRequestResponse($"Error while processing request"));;
            }

             return Ok(new ApiOkResponse());
        }
        #endregion

        #region Delete
        /// <summary>
        /// Set Restroom Request State status to active / passive
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _restroomrequestStateService.Delete(id);
            }
            catch (Exception )
            {
                 return BadRequest(new ApiBadRequestResponse($"Error while processing request"));;
            }

             return Ok(new ApiOkResponse($"Deleted"));
        }
        #endregion
    }
}