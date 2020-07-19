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
    [Route("api/dietaries")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DietaryController : ControllerBase
    {
        private readonly IDietaryService _dietaryService;

        public DietaryController(IDietaryService dietaryService)
        {
            _dietaryService = dietaryService ?? throw new ArgumentNullException(nameof(dietaryService));
        }

        #region Read
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DietaryModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<DietaryModel>>> Get([FromQuery] PageSearchArgs request)
        {
            try
            {
                var result = await _dietaryService.Search(request);
                JToken _jtoken = TokenService.CreateJToken(result, request.Props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<DietaryModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<DietaryModel>>> Get(int id)
        {
            var temp = await _dietaryService.GetById(id);

            return Ok(temp);
        }
        #endregion

        #region Create
        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] DietaryModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _dietaryService.Create(model);
                if (result != null)
                {
                    return Created($"api/dietaries/{result.Id}", result);
                }
            }

            return BadRequest(ModelState);
        }
        #endregion

        #region Update
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(DietaryModel model)
        {
            //TODO: Add you code here
            try
            {
                if (ModelState.IsValid)
                {
                    await _dietaryService.Update(model);
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
                await _dietaryService.Delete(id);
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