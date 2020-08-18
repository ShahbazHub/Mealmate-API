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
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/menuitemoptions")]
    [ApiValidationFilter]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuItemOptionController : ControllerBase
    {
        private readonly IMenuItemOptionService _menuItemOptionService;

        public MenuItemOptionController(
            IMenuItemOptionService menuItemOptionService
            )
        {
            _menuItemOptionService = menuItemOptionService ?? throw new ArgumentNullException(nameof(menuItemOptionService));
        }

        #region Read
        [Route("{menuItemId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<MenuItemOptionModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemOptionModel>>> Get(int menuItemId, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var MenuItemOptions = await _menuItemOptionService.Search(menuItemId, request);
                JToken _jtoken = TokenService.CreateJToken(MenuItemOptions, request.Props);
                return Ok(new ApiOkResponse(_jtoken));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        [Route("single/{menuItemOptionId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(MenuItemOptionModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MenuItemOptionModel>> Get(int menuItemOptionId)
        {
            try
            {
                var temp = await _menuItemOptionService.GetById(menuItemOptionId);
                if (temp == null)
                {
                    return NotFound(new ApiNotFoundResponse($"Resource with id {menuItemOptionId} no more exists"));
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
        [HttpPost]
        [ProducesResponseType(typeof(MenuItemOptionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MenuItemOptionModel>> Create([FromBody] MenuItemOptionModel request)
        {

            var commandResult = await _menuItemOptionService.Create(request);

            return Ok(commandResult);
        }
        #endregion

        #region Update
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(MenuItemOptionModel request)
        {
            await _menuItemOptionService.Update(request);
             return Ok(new ApiOkResponse());
        }
        #endregion

        #region Delete
        [HttpDelete("{menuItemOptionId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int menuItemOptionId)
        {
            await _menuItemOptionService.Delete(menuItemOptionId);
             return Ok(new ApiOkResponse());
        }
        #endregion

    }
}
