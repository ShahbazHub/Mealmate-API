using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Paging;


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

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
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MenuItemModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemModel>>> Get(int menuId)
        {
            var MenuItems = await _menuItemService.Get(menuId);
            return Ok(MenuItems);
        }
        #endregion

        #region Create
        [Route("[action]")]
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
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(MenuItemModel request)
        {
            await _menuItemService.Update(request);
            return Ok();
        }
        #endregion

        #region Delete
        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int menuItemId)
        {
            await _menuItemService.Delete(menuItemId);
            return Ok();
        }
        #endregion

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(IPagedList<MenuItemModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IPagedList<MenuItemModel>>> SearchMenuItems(SearchPageRequest request)
        //{
        //    var MenuItemPagedList = await _menuItemService.SearchMenuItems(request.Args);

        //    return Ok(MenuItemPagedList);
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(MenuItemModel), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<MenuItemModel>> GetMenuItemById(GetMenuItemByIdRequest request)
        //{
        //    var MenuItem = await _menuItemService.GetMenuItemById(request.Id);

        //    return Ok(MenuItem);
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(IEnumerable<MenuItemModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<MenuItemModel>>> GetMenuItemsByName(GetResturantsByNameRequest request)
        //{
        //    var MenuItems = await _menuItemService.GetMenuItemsByName(request.Name);

        //    return Ok(MenuItems);
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(IEnumerable<MenuItemModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<MenuItemModel>>> GetMenuItemsByCategoryId(GetMenuItemsByCategoryIdRequest request)
        //{
        //    var MenuItems = await _menuItemService.GetMenuItemsByCategoryId(request.CategoryId);

        //    return Ok(MenuItems);
        //}
    }
}
