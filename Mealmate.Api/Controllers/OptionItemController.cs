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
    [Route("api/optionitems")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OptionItemController : ControllerBase
    {
        private readonly IOptionItemService _optionItemService;

        public OptionItemController(IOptionItemService optionItemService)
        {
            _optionItemService = optionItemService ?? throw new ArgumentNullException(nameof(optionItemService));
        }

        #region Read
        [HttpGet("{branchId}/{isActive}")]
        [ProducesResponseType(typeof(IEnumerable<OptionItemModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OptionItemModel>>> Get(
            int branchId, int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var result = await _optionItemService.Search(branchId, isActive, request);
                JToken _jtoken = TokenService.CreateJToken(result, request.Props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("single/{optionItemId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<OptionItemModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OptionItemModel>>> Get(int optionItemId)
        {
            try
            {
                var temp = await _optionItemService.GetById(optionItemId);

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
        [ProducesResponseType(typeof(IEnumerable<OptionItemModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Create([FromBody] OptionItemCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _optionItemService.Create(model);
                if (result != null)
                {
                    return Created($"api/optionItems/{result.Id}", result);
                }
            }

            return BadRequest(ModelState);
        }
        [HttpPost("bulk")]
        [ProducesResponseType(typeof(IEnumerable<OptionItemModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Create([FromBody] OptionItemDetailCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _optionItemService.Create(model);
                if (result != null)
                {
                    return Created($"api/optionItems/{result.Id}", result);
                }
            }

            return BadRequest(ModelState);
        }
        #endregion

        #region Update
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, OptionItemUpdateModel model)
        {
            //TODO: Add you code here
            try
            {
                if (ModelState.IsValid)
                {
                    await _optionItemService.Update(id, model);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [Route("bulk/{id}")]
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, OptionItemDetailUpdateModel model)
        {
            //TODO: Add you code here
            try
            {
                if (ModelState.IsValid)
                {
                    await _optionItemService.Update(id, model);
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
        [HttpDelete("{optionItemId}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int optionItemId)
        {
            try
            {
                await _optionItemService.Delete(optionItemId);
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