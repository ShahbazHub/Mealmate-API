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
    [Route("api/restroomrequests")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RestroomRequestController : ControllerBase
    {
        private readonly IRestroomRequestService _restroomRequestService;

        public RestroomRequestController(IRestroomRequestService restroomRequestService)
        {
            _restroomRequestService = restroomRequestService ?? throw new ArgumentNullException(nameof(restroomRequestService));
        }

        #region Read
        /// <summary>
        /// List all restroom requests of a specific customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpGet("list/{customerId}")]
        [ProducesResponseType(typeof(IEnumerable<RestroomRequestModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RestroomRequestModel>>> Get(
            int customerId, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var result = await _restroomRequestService.Search(customerId, request);
                JToken _jtoken = TokenService.CreateJToken(result, request.Props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get a specific restroom request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<RestroomRequestModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RestroomRequestModel>>> Get(int id)
        {
            try
            {
                var temp = await _restroomRequestService.GetById(id);
                if (temp == null)
                {
                    return NotFound($"Resource with id {id} no more exists");
                }

                return Ok(temp);
            }
            catch (Exception)
            {
                return BadRequest("Error while processing your request");
            }
        }
        #endregion

        #region Create
        /// <summary>
        /// Create a new restroom request
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] RestroomRequestCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _restroomRequestService.Create(model);
                if (result != null)
                {
                    return Created($"api/restroomRequests/{result.Id}", result);
                }
            }

            return BadRequest(ModelState);
        }
        #endregion

        #region Update
        /// <summary>
        /// Update an existing restroom request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, RestroomRequestUpdateModel model)
        {
            //TODO: Add you code here
            try
            {
                if (ModelState.IsValid)
                {
                    await _restroomRequestService.Update(id, model);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete an existing restroom request
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
                await _restroomRequestService.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
        #endregion
    }
}