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
    [Route("api/menus")]
    [ApiValidationFilter]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService ?? throw new ArgumentNullException(nameof(menuService)); ;
        }


        #region Read
        [HttpGet()]
        [Route("{branchId}/{isActive}")]
        [ProducesResponseType(typeof(IEnumerable<MenuModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<MenuModel>>> Get(
            int branchId, int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var Menus = await _menuService.Search(branchId, isActive, request);
                JToken _jtoken = TokenService.CreateJToken(Menus, request.Props);
                return Ok(new ApiOkResponse(new { _jtoken }));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        [Route("single/{menuId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(MenuModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MenuModel>> Get(int menuId)
        {
            try
            {
                var Menu = await _menuService.GetById(menuId);
                if (Menu == null)
                {
                    return NotFound(new ApiNotFoundResponse($"Resource Not Found"));
                }
                return Ok(Menu);
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }
        #endregion

        #region Create
        [HttpPost]
        [ProducesResponseType(typeof(MenuModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MenuModel>> Create([FromBody] MenuCreateModel request)
        {
            var result = await _menuService.Create(request);
             return Ok(new ApiOkResponse(new { result }));;
        }
        #endregion

        #region Update
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, [FromBody] MenuUpdateModel request)
        {
            await _menuService.Update(id, request);
            return Ok(new ApiOkResponse());
        }
        #endregion

        #region Delete
        [HttpDelete("{menuId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int menuId)
        {
            await _menuService.Delete(menuId);
             return Ok(new ApiOkResponse());
        }
        #endregion
    }
}