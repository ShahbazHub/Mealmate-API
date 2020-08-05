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
    [Consumes("application/json")]
    [Produces("application/json")]
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
        [Route("{menuId}/{isActive}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<MenuItemModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemModel>>> Get(
            int menuId, int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var MenuItems = await _menuItemService.Search(menuId, isActive, request);
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
                var temp = await _menuItemService.GetById(menuItemId);
                if (temp == null)
                {
                    return NotFound($"Resource with id {menuItemId} no more exists");
                }
                return Ok(temp);
            }
            catch (Exception)
            {
                return BadRequest("Error while processing your request");
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
        public async Task<ActionResult<MenuItemModel>> Create([FromBody] MenuItemCreateModel request)
        {
            try
            {
                var result = await _menuItemService.Create(request);
                return Created($"api/menuitem/{result.Id}", result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("bulk")]
        [ProducesResponseType(typeof(MenuItemModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MenuItemModel>> Create([FromBody] MenuItemDetailCreateModel request)
        {
            try
            {
                var result = await _menuItemService.Create(request);
                return Created($"api/menuitem/{result.Id}", result);
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
        public async Task<ActionResult> Update(int id, MenuItemUpdateModel request)
        {
            try
            {
                await _menuItemService.Update(id, request);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("bulk/{id}")]
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, MenuItemDetailUpdateModel model)
        {
            //TODO: Add you code here
            try
            {
                if (ModelState.IsValid)
                {
                    await _menuItemService.Update(id, model);
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
        [HttpDelete("{menuItemId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int menuItemId)
        {
            try
            {
                await _menuItemService.Delete(menuItemId);
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
