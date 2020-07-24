using System;
using System.Collections.Generic;
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
    [Route("api/allergens")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AllergenController : ControllerBase
    {
        private readonly IAllergenService _allergenService;

        public AllergenController(IAllergenService allergenService)
        {
            _allergenService = allergenService ?? throw new ArgumentNullException(nameof(allergenService));
        }

        #region Read
        [HttpGet("list/{isActive}")]
        [ProducesResponseType(typeof(IEnumerable<AllergenModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<AllergenModel>>> Get(
            int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var result = await _allergenService.Search(isActive, request);
                JToken _jtoken = TokenService.CreateJToken(result, request.Props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<AllergenModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<AllergenModel>>> Get(int id)
        {
            var temp = await _allergenService.GetById(id);

            return Ok(temp);
        }
        #endregion

        #region Create
        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] AllergenCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _allergenService.Create(model);
                if (result != null)
                {
                    return Created($"api/allergens/{result.Id}", result);
                }
            }

            return BadRequest(ModelState);
        }
        #endregion

        #region Update
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, AllergenUpdateModel model)
        {
            //TODO: Add you code here
            try
            {
                if (ModelState.IsValid)
                {
                    await _allergenService.Update(id, model);
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