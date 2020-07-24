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
    [Route("api/optionItemDietaries")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OptionItemDietaryController : ControllerBase
    {
        private readonly IOptionItemDietaryService _optionItemDietaryService;

        public OptionItemDietaryController(
            IOptionItemDietaryService optionItemDietaryService
            )
        {
            _optionItemDietaryService = optionItemDietaryService ?? throw new ArgumentNullException(nameof(optionItemDietaryService));
        }

        #region Read
        [Route("{optionItemId}/{isActive}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<OptionItemDietaryModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OptionItemDietaryModel>>> Get(
            int optionItemId, int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var OptionItemDietarys = await _optionItemDietaryService.Search(optionItemId, isActive, request);
                JToken _jtoken = TokenService.CreateJToken(OptionItemDietarys, request.Props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("single/{optionItemDietaryId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(OptionItemDietaryModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OptionItemDietaryModel>> Get(int optionItemDietaryId)
        {
            try
            {
                var temp = await _optionItemDietaryService.Get(optionItemDietaryId);
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
        [ProducesResponseType(typeof(OptionItemDietaryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<OptionItemDietaryModel>> Create([FromBody] OptionItemDietaryCreateModel request)
        {
            try
            {
                var result = await _optionItemDietaryService.Create(request);
                return Created($"api/optionitemdietaries/{result.Id}", result);
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
        public async Task<ActionResult> Update(int id, [FromBody] OptionItemDietaryUpdateModel request)
        {
            try
            {
                await _optionItemDietaryService.Update(id, request);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{optionItemDietaryId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int optionItemDietaryId)
        {
            await _optionItemDietaryService.Delete(optionItemDietaryId);
            return Ok();
        }
        #endregion

    }
}
