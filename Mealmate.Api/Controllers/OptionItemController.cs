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
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/optionitems")]
    [ApiValidationFilter]
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
                return Ok(new ApiOkResponse(_jtoken));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        [Route("single/{optionItemId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(OptionItemModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OptionItemModel>> Get(int optionItemId)
        {
            try
            {
                var temp = await _optionItemService.GetById(optionItemId);
                if (temp == null)
                {
                    return NotFound(new ApiNotFoundResponse($"Resource with id {optionItemId} no more exists"));
                }
                 return Ok(new ApiOkResponse(temp));
            }
            catch (Exception)
            {
                 return BadRequest(new ApiBadRequestResponse($"Error while processing request"));;
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

             return BadRequest(new ApiBadRequestResponse(ModelState, $"Error while processing request"));;
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

             return BadRequest(new ApiBadRequestResponse(ModelState, $"Error while processing request"));;
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
            catch (Exception )
            {
                 return BadRequest(new ApiBadRequestResponse($"Error while processing request"));;
            }

             return Ok(new ApiOkResponse());
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
            catch (Exception )
            {
                 return BadRequest(new ApiBadRequestResponse($"Error while processing request"));;
            }

             return Ok(new ApiOkResponse());
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
            catch (Exception )
            {
                 return BadRequest(new ApiBadRequestResponse($"Error while processing request"));;
            }

             return Ok(new ApiOkResponse($"Deleted"));
        }
        #endregion
    }
}