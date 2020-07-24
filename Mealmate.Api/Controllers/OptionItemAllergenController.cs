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
using System.Net;
using System.Threading.Tasks;

namespace Mealmate.Api.Controllers
{
    [Route("api/optionItemAllergens")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OptionItemAllergenController : ControllerBase
    {
        private readonly IOptionItemAllergenService _optionItemAllergenService;

        public OptionItemAllergenController(
            IOptionItemAllergenService optionItemAllergenService
            )
        {
            _optionItemAllergenService = optionItemAllergenService ?? throw new ArgumentNullException(nameof(optionItemAllergenService));
        }

        #region Read
        [Route("{optionItemId}/{isActive}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<OptionItemAllergenModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OptionItemAllergenModel>>> Get(
            int optionItemId, int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var OptionItemAllergens = await _optionItemAllergenService.Search(optionItemId, isActive, request);
                JToken _jtoken = TokenService.CreateJToken(OptionItemAllergens, request.Props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("single/{optionItemAllergenId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(OptionItemAllergenModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OptionItemAllergenModel>> Get(int optionItemAllergenId)
        {
            try
            {
                var temp = await _optionItemAllergenService.Get(optionItemAllergenId);
                return Ok(temp);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Create
        [HttpPost()]
        [ProducesResponseType(typeof(OptionItemAllergenModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<OptionItemAllergenModel>> Create([FromBody] OptionItemAllergenCreateModel request)
        {
            try
            {
                var result = await _optionItemAllergenService.Create(request);
                return Created($"api/optionitemallergens/{result.Id}", result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Update
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, [FromBody] OptionItemAllergenUpdateModel request)
        {
            try
            {
                await _optionItemAllergenService.Update(id, request);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{optionItemAllergenId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int optionItemAllergenId)
        {
            try
            {
                await _optionItemAllergenService.Delete(optionItemAllergenId);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

    }
}
