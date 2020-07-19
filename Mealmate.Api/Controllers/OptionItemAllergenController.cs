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
        [Route("{optionItemId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<OptionItemAllergenModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OptionItemAllergenModel>>> Get(int optionItemId, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var OptionItemAllergens = await _optionItemAllergenService.Search(optionItemId, request);
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
        public async Task<ActionResult<OptionItemAllergenModel>> Create([FromBody] OptionItemAllergenModel request)
        {
            var result = await _optionItemAllergenService.Create(request);
            return Created($"api/optionitemallergens/{result.Id}", result);
        }
        #endregion

        #region Update
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update([FromBody] OptionItemAllergenModel request)
        {
            await _optionItemAllergenService.Update(request);
            return Ok();
        }
        #endregion

        #region Delete
        [HttpDelete("{optionItemAllergenId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int optionItemAllergenId)
        {
            await _optionItemAllergenService.Delete(optionItemAllergenId);
            return Ok();
        }
        #endregion

    }
}
