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
    [ApiValidationFilter()]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/allergens")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AllergenController : ControllerBase
    {
        private readonly IAllergenService _allergenService;

        public AllergenController(IAllergenService allergenService)
        {
            _allergenService = allergenService ?? throw new ArgumentNullException(nameof(allergenService));
        }

        #region Read

        [AllowAnonymous]
        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<AllergenListModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<AllergenListModel>>> Get()
        {
            try
            {
                IEnumerable<AllergenListModel> data = null;
                var result = await _allergenService.Get();
                if (result != null)
                {
                    data = result.Select(p => new AllergenListModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        IsActive = p.IsActive
                    });
                }
                return Ok(new ApiOkResponse(new { data }));

            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        /// <summary>
        /// List all allergens as per status
        /// </summary>
        /// <param name="isActive"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("list/{isActive}")]
        [ProducesResponseType(typeof(IEnumerable<AllergenModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<AllergenModel>>> Get(
            int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var result = await _allergenService.Search(isActive, request);
                JToken _jtoken = TokenService.CreateJToken(result, request.Props);

                return Ok(new ApiOkResponse(new { _jtoken }));

            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        /// <summary>
        /// Get a single allergen for a specific id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<AllergenModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<AllergenModel>>> Get(int id)
        {
            try
            {
                var data = await _allergenService.GetById(id);
                if (data == null)
                {
                    return NotFound($"Resource with id {id} no more exists");
                }
                return Ok(new ApiOkResponse(new { data }));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse("Error while processing request"));
            }
        }
        #endregion

        #region Create
        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] AllergenCreateModel model)
        {
            try
            {
                var result = await _allergenService.Create(model);
                if (result != null)
                {
                    return Created($"api/allergens/{result.Id}", new ApiCreatedResponse(result));
                }
                else
                {
                    return BadRequest(new ApiBadRequestResponse($"Error while creating resource"));
                }
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }
        #endregion

        #region Update
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, AllergenUpdateModel model)
        {
            try
            {
                await _allergenService.Update(id, model);
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiBadRequestResponse(ex.Message));
            }

            return Ok(new ApiOkResponse($"Data updated successfully"));
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
                await _allergenService.Delete(id);
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