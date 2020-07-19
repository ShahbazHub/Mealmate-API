using Mealmate.Api.Helpers;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Mealmate.Api.Requests;
using Mealmate.Core.Paging;

namespace Mealmate.Api.Controllers
{
    [Route("api/menuitems")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(
            IMenuItemService menuItemService
            )
        {
            _menuItemService = menuItemService ?? throw new ArgumentNullException(nameof(menuItemService));
        }

        #region Read
        [Route("{menuId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<MenuItemModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemModel>>> Get(int menuId, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var MenuItems = await _menuItemService.Search(menuId, request);
                JToken _jtoken = TokenService.CreateJToken(MenuItems, request.Props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("single/{menuItemId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(MenuItemModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MenuItemModel>> Get(int menuItemId)
        {
            try
            {
                var MenuItem = await _menuItemService.GetById(menuItemId);
                return Ok(MenuItem);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("filter")]
        [HttpPost()]
        [ProducesResponseType(typeof(IEnumerable<MenuItemModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemModel>>> Get(
            [FromBody] MenuFilterRequest filterRequest)
        {
            try
            {
                //TODO: filtering the menu items having allergens / dietaries
                var MenuItems = await _menuItemService.Get(filterRequest.allergenIds, filterRequest.dietaryIds);
                return Ok(MenuItems);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Create
        [HttpPost]
        [ProducesResponseType(typeof(MenuItemModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MenuItemModel>> Create(MenuItemModel request)
        {
            var result = await _menuItemService.Create(request);
            return Created($"api/menuitem/{result.Id}", result);
        }
        #endregion

        #region Update
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(MenuItemModel request)
        {
            await _menuItemService.Update(request);
            return Ok();
        }
        #endregion

        #region Delete
        [HttpDelete("{menuItemId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int menuItemId)
        {
            await _menuItemService.Delete(menuItemId);
            return Ok();
        }
        #endregion

    }
}
