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
    [Route("api/menuitemoptions")]
    [ApiController]
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
        [Route("{menuItemId}/{optionItemId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<MenuItemOptionModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemOptionModel>>> Get(int menuItemId,int optionItemId,string props)
        {
            try
            {
                var MenuItemOptions = await _menuItemOptionService.Get(menuItemId, optionItemId);
                JToken _jtoken = TokenService.CreateJToken(MenuItemOptions, props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Create
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(MenuItemOptionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MenuItemOptionModel>> Create(MenuItemOptionModel request)
        {

            var commandResult = await _menuItemOptionService.Create(request);

            return Ok(commandResult);
        }
        #endregion

        #region Update
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(MenuItemOptionModel request)
        {
            await _menuItemOptionService.Update(request);
            return Ok();
        }
        #endregion

        #region Delete
        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int meneItemOptionId)
        {
            await _menuItemOptionService.Delete(meneItemOptionId);
            return Ok();
        }
        #endregion

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(IPagedList<MenuItemOptionModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IPagedList<MenuItemOptionModel>>> SearchMenuItemOptions(SearchPageRequest request)
        //{
        //    var MenuItemOptionPagedList = await _menuItemOptionService.SearchMenuItemOptions(request.Args);

        //    return Ok(MenuItemOptionPagedList);
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(MenuItemOptionModel), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<MenuItemOptionModel>> GetMenuItemOptionById(GetMenuItemOptionByIdRequest request)
        //{
        //    var MenuItemOption = await _menuItemOptionService.GetMenuItemOptionById(request.Id);

        //    return Ok(MenuItemOption);
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(IEnumerable<MenuItemOptionModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<MenuItemOptionModel>>> GetMenuItemOptionsByName(GetResturantsByNameRequest request)
        //{
        //    var MenuItemOptions = await _menuItemOptionService.GetMenuItemOptionsByName(request.Name);

        //    return Ok(MenuItemOptions);
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(IEnumerable<MenuItemOptionModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<MenuItemOptionModel>>> GetMenuItemOptionsByCategoryId(GetMenuItemOptionsByCategoryIdRequest request)
        //{
        //    var MenuItemOptions = await _menuItemOptionService.GetMenuItemOptionsByCategoryId(request.CategoryId);

        //    return Ok(MenuItemOptions);
        //}
    }
}
