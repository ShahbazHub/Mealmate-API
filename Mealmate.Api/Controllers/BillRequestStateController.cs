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
    [Route("api/billRequeststates")]
    [ApiValidationFilter]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BillRequestStateController : ControllerBase
    {
        private readonly IBillRequestStateService _billRequestStateService;

        public BillRequestStateController(IBillRequestStateService billRequestStateService)
        {
            _billRequestStateService = billRequestStateService ?? throw new ArgumentNullException(nameof(billRequestStateService));
        }

        #region Read
        /// <summary>
        /// List all Bill Request States
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<BillRequestStateModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BillRequestStateModel>>> Get()
        {
            try
            {
                var result = await _billRequestStateService.Get();
                if (result != null)
                {
                    result = result.Where(p => p.IsActive == true);
                }
                return Ok(new ApiOkResponse(result));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        /// <summary>
        /// List all Bill Request States with specific state (0: Passive, 1: Active, 2: All)
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("list/{isActive}")]
        [ProducesResponseType(typeof(IEnumerable<BillRequestStateModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BillRequestStateModel>>> Get(
            int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var result = await _billRequestStateService.Search(isActive, request);
                JToken _jtoken = TokenService.CreateJToken(result, request.Props);
                return Ok(new ApiOkResponse(_jtoken));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        /// <summary>
        /// Get a specific Bill Request State
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<BillRequestStateModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BillRequestStateModel>>> Get(int id)
        {
            try
            {
                var temp = await _billRequestStateService.GetById(id);
                if (temp == null)
                {
                    return NotFound(new ApiNotFoundResponse($"Resource Not Found"));
                }

                return Ok(new ApiOkResponse(temp));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }
        #endregion

        #region Create
        /// <summary>
        /// Create a new Bill Request State
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] BillRequestStateCreateModel model)
        {
            var result = await _billRequestStateService.Create(model);
            if (result != null)
            {
                return Created($"api/billRequestStates/{result.Id}", new ApiCreatedResponse(result));
            }

            return BadRequest(new ApiBadRequestResponse(ModelState, $"Error while processing request"));
        }
        #endregion

        #region Update
        /// <summary>
        /// Update an existing Bill Request State
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, [FromBody] BillRequestStateUpdateModel model)
        {
            try
            {
                await _billRequestStateService.Update(id, model);
            }
            catch (Exception )
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }

            return Ok(new ApiOkResponse($"Updated"));
        }
        #endregion

        #region Delete
        /// <summary>
        /// Set Bill Request State status to active / passive
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
                await _billRequestStateService.Delete(id);
            }
            catch (Exception )
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }

            return Ok(new ApiOkResponse($"Deleted"));
        }
        #endregion
    }
}