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
    [Route("api/contactrequeststates")]
    [ApiValidationFilter]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ContactRequestStateController : ControllerBase
    {
        private readonly IContactRequestStateService _contactrequestStateService;

        public ContactRequestStateController(IContactRequestStateService contactrequestStateService)
        {
            _contactrequestStateService = contactrequestStateService ?? throw new ArgumentNullException(nameof(contactrequestStateService));
        }

        #region Read
        /// <summary>
        /// List all Contact Request States
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<ContactRequestStateModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ContactRequestStateModel>>> Get()
        {
            try
            {
                var result = await _contactrequestStateService.Get();
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
        /// List all Contact Request States with specific state (0: Passive, 1: Active, 2: All)
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("list/{isActive}")]
        [ProducesResponseType(typeof(IEnumerable<ContactRequestStateModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ContactRequestStateModel>>> Get(
            int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var result = await _contactrequestStateService.Search(isActive, request);
                JToken _jtoken = TokenService.CreateJToken(result, request.Props);
                return Ok(new ApiOkResponse(new { _jtoken }));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        /// <summary>
        /// Get a specific Contact Request State
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<ContactRequestStateModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<ContactRequestStateModel>>> Get(int id)
        {
            try
            {
                var temp = await _contactrequestStateService.GetById(id);
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
        /// Create a new Contact Request State
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] ContactRequestStateCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _contactrequestStateService.Create(model);
                if (result != null)
                {
                    return Created($"api/contactrequestStates/{result.Id}", result);
                }
            }

             return BadRequest(new ApiBadRequestResponse(ModelState, $"Error while processing request"));;
        }
        #endregion

        #region Update
        /// <summary>
        /// Update an existing Contact Request State
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, ContactRequestStateUpdateModel model)
        {
            //TODO: Add you code here
            try
            {
                if (ModelState.IsValid)
                {
                    await _contactrequestStateService.Update(id, model);
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
        /// Set Contact Request State status to active / passive
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
                await _contactrequestStateService.Delete(id);
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