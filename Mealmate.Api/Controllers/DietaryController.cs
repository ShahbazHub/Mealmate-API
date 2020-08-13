using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Mealmate.Api.Helpers;
using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Paging;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Mealmate.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/dietaries")]
    [ApiValidationFilter]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DietaryController : ControllerBase
    {
        private readonly IDietaryService _dietaryService;

        public DietaryController(IDietaryService dietaryService)
        {
            _dietaryService = dietaryService ?? throw new ArgumentNullException(nameof(dietaryService));
        }

        #region Read
        [AllowAnonymous]
        [HttpGet("list/{isActive}")]
        [ProducesResponseType(typeof(IEnumerable<DietaryModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<DietaryModel>>> Get(
            int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var result = await _dietaryService.Search(isActive, request);
                JToken _jtoken = TokenService.CreateJToken(result, request.Props);
                return Ok(new ApiOkResponse(new { _jtoken }));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<DietaryModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<DietaryModel>>> Get(int id)
        {
            try
            {
                var temp = await _dietaryService.GetById(id);
                if (temp == null)
                {
                    return NotFound(new ApiNotFoundResponse($"Resource with id {id} no more exists"));
                }
                 return Ok(new ApiOkResponse(new { temp }));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }
        #endregion

        #region Create
        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] DietaryCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _dietaryService.Create(model);
                if (result != null)
                {
                    return Created($"api/dietaries/{result.Id}", result);
                }
            }

             return BadRequest(new ApiBadRequestResponse(ModelState, $"Error while processing request"));;
        }
        #endregion

        #region Update
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, DietaryUpdateModel model)
        {
            //TODO: Add you code here
            try
            {
                if (ModelState.IsValid)
                {
                    await _dietaryService.Update(id, model);
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
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _dietaryService.Delete(id);
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