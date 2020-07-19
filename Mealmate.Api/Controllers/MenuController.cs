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
    [Route("api/menus")]
    [ApiController]
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
        [Route("{branchId}")]
        [ProducesResponseType(typeof(IEnumerable<MenuModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<IEnumerable<MenuModel>>> Get(int branchId, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var Menus = await _menuService.Search(branchId, request);
                JToken _jtoken = TokenService.CreateJToken(Menus, request.Props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
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
                return Ok(Menu);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Create
        [HttpPost]
        [ProducesResponseType(typeof(MenuModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MenuModel>> Create([FromBody]MenuModel request)
        {
            var result = await _menuService.Create(request);
            return Ok(result);
        }
        #endregion

        #region Update
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update([FromBody]MenuModel request)
        {
            await _menuService.Update(request);
            return Ok();
        }
        #endregion

        #region Delete
        [HttpDelete("{menuId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int menuId)
        {
            await _menuService.Delete(menuId);
            return Ok();
        }
        #endregion
    }
}