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
        [HttpGet("{menuId}")]
        [ProducesResponseType(typeof(IEnumerable<MenuItemModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemModel>>> Get(int menuId, string props)
        {
            try
            {
                var MenuItems = await _menuItemService.Get(menuId);
                JToken _jtoken = TokenService.CreateJToken(MenuItems, props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{menuItemId}")]
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
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<MenuItemModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemModel>>> Get(
            [FromBody] MenuFilterRequest filterRequest)
        {
            try
            {
                var MenuItem = await _menuItemService.GetById(1);
                return Ok(MenuItem);
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
