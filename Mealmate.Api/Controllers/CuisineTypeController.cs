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
    [Route("api/cuisinetypes")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuisineTypeController : ControllerBase
    {
        private readonly ICuisineTypeService _cuisineTypeService;

        public CuisineTypeController(ICuisineTypeService cuisineTypeService)
        {
            _cuisineTypeService = cuisineTypeService ?? throw new ArgumentNullException(nameof(cuisineTypeService));
        }

        #region Read
        [HttpGet("list/{isActive}")]
        [ProducesResponseType(typeof(IEnumerable<CuisineTypeModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CuisineTypeModel>>> Get(
            int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var result = await _cuisineTypeService.Search(isActive, request);
                JToken _jtoken = TokenService.CreateJToken(result, request.Props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<CuisineTypeModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<CuisineTypeModel>>> Get(int id)
        {
            try
            {
                var temp = await _cuisineTypeService.GetById(id);
                if (temp == null)
                {
                    return NotFound($"Resource with id {id} no more exists");
                }

                return Ok(temp);
            }
            catch (Exception)
            {
                return BadRequest("Error while processing request");
            }
        }
        #endregion

        #region Create
        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] CuisineTypeCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _cuisineTypeService.Create(model);
                if (result != null)
                {
                    return Created($"api/cuisinetypes/{result.Id}", result);
                }
            }

            return BadRequest(ModelState);
        }
        #endregion

        #region Update
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, CuisineTypeUpdateModel model)
        {
            //TODO: Add you code here
            try
            {
                if (ModelState.IsValid)
                {
                    await _cuisineTypeService.Update(id, model);
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
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _cuisineTypeService.Delete(id);
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