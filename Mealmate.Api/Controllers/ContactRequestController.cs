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
    [Route("api/contactrequests")]
    [ApiValidationFilter]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContactRequestController : ControllerBase
    {
        private readonly IContactRequestService _contactRequestService;
        private readonly UserManager<User> _userManager;
        private readonly ITableService _tableService;

        public ContactRequestController(
            UserManager<User> userManager,
            IContactRequestService contactRequestService,
            ITableService tableService)
        {
            _userManager = userManager;
            _tableService = tableService ?? throw new ArgumentNullException(nameof(tableService));
            _contactRequestService = contactRequestService ?? throw new ArgumentNullException(nameof(contactRequestService));
        }

        #region Read
        /// <summary>
        /// List all contact requests for a branch with specific state
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="contactRequestStateId"></param>
        /// <returns></returns>
        [HttpGet("list/{branchId}/state/{contactRequestStateId}")]
        [ProducesResponseType(typeof(IEnumerable<ContactRequestModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ContactRequestModel>>> Get(
            int branchId, int contactRequestStateId)
        {
            try
            {
                var result = await _contactRequestService.Get(branchId, contactRequestStateId);
                return Ok(new ApiOkResponse(new { result })); ;
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        /// <summary>
        /// List all contact requests of a specific branch
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpGet("list/{branchId}")]
        [ProducesResponseType(typeof(IEnumerable<ContactRequestModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ContactRequestModel>>> Get(
            int branchId, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var result = await _contactRequestService.Search(branchId, request);
                JToken _jtoken = TokenService.CreateJToken(result, request.Props);
                return Ok(new ApiOkResponse(new { _jtoken }));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        /// <summary>
        /// Get a specific contact request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<ContactRequestModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ContactRequestModel>>> Get(int id)
        {
            try
            {
                var temp = await _contactRequestService.GetById(id);
                if (temp == null)
                {
                    return NotFound(new ApiNotFoundResponse($"Resource with id {id} no more exists"));
                }

                return Ok(new ApiOkResponse(new { temp }));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request")); ;
            }
        }
        #endregion

        #region Create
        /// <summary>
        /// Create a new contact request
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] ContactRequestCreateModel model)
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

                var result = await _contactRequestService.Create(model);
                if (result != null)
                {
                    return Created($"api/contactRequests/{result.Id}", new ApiCreatedResponse(result));
                }
            }

            return BadRequest(new ApiBadRequestResponse(ModelState, $"Error while processing request")); ;
        }
        #endregion

        #region Update
        /// <summary>
        /// Update an existing contact request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, ContactRequestUpdateModel model)
        {
            try
            {
                await _contactRequestService.Update(id, model);
            }
            catch (Exception )
            {
                 return BadRequest(new ApiBadRequestResponse($"Error while processing request"));;
            }

            return Ok(new ApiOkResponse($"Updated"));
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete an existing contact request
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
                await _contactRequestService.Delete(id);
            }
            catch (Exception)
            {
                 return BadRequest(new ApiBadRequestResponse($"Error while processing request"));;
            }

            return Ok(new ApiOkResponse($"Deleted"));
        }
        #endregion
    }
}