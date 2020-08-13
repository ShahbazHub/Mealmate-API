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
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Mealmate.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/branches")]
    [ApiValidationFilter]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        #region Read
        /// <summary>
        /// List all branches in a specific restaurant
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="isActive"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{restaurantId}/{isActive}")]
        [ProducesResponseType(typeof(IEnumerable<BranchModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BranchModel>>> Get(
            int restaurantId, int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var Branches = await _branchService.Search(restaurantId, isActive, request);
                JToken _jtoken = TokenService.CreateJToken(Branches, request.Props);
                return Ok(new ApiOkResponse(new { _jtoken }));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        /// <summary>
        /// Get a specific branch
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("single/{branchId}")]
        [ProducesResponseType(typeof(IEnumerable<BranchModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BranchModel>>> Get(int branchId)
        {
            try
            {
                var model = await _branchService.GetById(branchId);
                if (model == null)
                {
                    return NotFound($"Resource with id {branchId} no more exists");
                }

                return Ok(new ApiOkResponse(new { model }));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }
        #endregion

        #region Create
        /// <summary>
        /// Create a new branch
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BranchModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<BranchModel>> Create(BranchCreateModel request)
        {
            try
            {
                var result = await _branchService.Create(request);
                return Ok(new ApiOkResponse(new { result }));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// Update an existing branch
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, BranchUpdateModel request)
        {
            try
            {
                await _branchService.Update(id, request);
                return Ok(new ApiOkResponse($"Updated"));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete branch by id
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        [HttpDelete("{branchId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int branchId)
        {
            try
            {
                await _branchService.Delete(branchId);
                return Ok(new ApiOkResponse($"Deleted"));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }
        #endregion
    }
}
