using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Mealmate.Api.Helpers;
using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities;
using Mealmate.Core.Entities.Lookup;
using Mealmate.Core.Paging;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Mealmate.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/billrequests")]
    [ApiValidationFilter]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BillRequestController : ControllerBase
    {
        private readonly IBillRequestService _billRequestService;
        private readonly UserManager<User> _userManager;
        private readonly ITableService _tableService;
        public BillRequestController(
            UserManager<User> userManager,
            IBillRequestService billRequestService,
            ITableService tableService)
        {
            _userManager = userManager;
            _tableService = tableService ?? throw new ArgumentNullException(nameof(tableService));
            _billRequestService = billRequestService ?? throw new ArgumentNullException(nameof(billRequestService));
        }

        #region Read
        /// <summary>
        /// Get list by specific branch and request state
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="billRequestStateId"></param>
        /// <returns></returns>
        [HttpGet("list/{branchId}/state/{billRequestStateId}")]
        [ProducesResponseType(typeof(IEnumerable<BillRequestModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BillRequestModel>>> Get(
            int branchId, int billRequestStateId)
        {
            try
            {
                var result = await _billRequestService.Get(branchId, billRequestStateId);
                return Ok(new ApiOkResponse(result));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        /// <summary>
        /// List all bill requests of a specific branch
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpGet("list/{branchId}")]
        [ProducesResponseType(typeof(IEnumerable<BillRequestModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BillRequestModel>>> Get(
            int branchId, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var result = await _billRequestService.Search(branchId, request);
                JToken _jtoken = TokenService.CreateJToken(result, request.Props);
                return Ok(new ApiOkResponse(_jtoken));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        /// <summary>
        /// Get a specific bill request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<BillRequestModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BillRequestModel>>> Get(int id)
        {
            try
            {
                var temp = await _billRequestService.GetById(id);
                if (temp == null)
                {
                    return NotFound(new ApiNotFoundResponse($"Resource with id {id} no more exists"));
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
        /// Create a new bill request
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] BillRequestCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = await _userManager.Users.FirstOrDefaultAsync(p => p.Id == model.CustomerId);
                if (customer == null)
                {
                    return NotFound(new ApiNotFoundResponse($"User does't exist"));
                }

                var table = await _tableService.GetById(model.TableId);
                if (table == null)
                {
                    return NotFound(new ApiNotFoundResponse($"Table doesn't exists"));
                }

                var result = await _billRequestService.Create(model);
                if (result != null)
                {
                    return Created($"api/billRequests/{result.Id}", new ApiCreatedResponse(result)); ;
                }
            }

            return BadRequest(new ApiBadRequestResponse(ModelState, $"Error while processing request"));
        }
        #endregion

        #region Update
        /// <summary>
        /// Update an existing bill request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, BillRequestUpdateModel model)
        {
            //TODO: Add you code here
            try
            {
                await _billRequestService.Update(id, model);
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }

            return Ok(new ApiOkResponse("Updated"));
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete an existing bill request
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
                await _billRequestService.Delete(id);
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }

            return Ok(new ApiOkResponse("Deleted"));
        }
        #endregion
    }
}