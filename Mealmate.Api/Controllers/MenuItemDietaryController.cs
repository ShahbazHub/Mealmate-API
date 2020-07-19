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
    [Route("api/menuItemDietaries")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuItemDietaryController : ControllerBase
    {
        private readonly IMenuItemDietaryService _menuItemDietaryService;

        public MenuItemDietaryController(
            IMenuItemDietaryService menuItemDietaryService
            )
        {
            _menuItemDietaryService = menuItemDietaryService ?? throw new ArgumentNullException(nameof(menuItemDietaryService));
        }

        #region Read
        [Route("{menuItemId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<MenuItemDietaryModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemDietaryModel>>> Get(int menuItemId, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var MenuItemDietarys = await _menuItemDietaryService.Search(menuItemId, request);
                JToken _jtoken = TokenService.CreateJToken(MenuItemDietarys, request.Props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("single/{menuItemDietaryId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(MenuItemDietaryModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MenuItemDietaryModel>> Get(int menuItemDietaryId)
        {
            try
            {
                var temp = await _menuItemDietaryService.Get(menuItemDietaryId);
                return Ok(temp);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Create
        [HttpPost]
        [ProducesResponseType(typeof(MenuItemDietaryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MenuItemDietaryModel>> Create([FromBody] MenuItemDietaryModel request)
        {
            var result = await _menuItemDietaryService.Create(request);
            return Created($"api/menuitemdietaries/{result.Id}", result);
        }
        #endregion

        #region Update
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update([FromBody] MenuItemDietaryModel request)
        {
            await _menuItemDietaryService.Update(request);
            return Ok();
        }
        #endregion

        #region Delete
        [HttpDelete("{menuItemDietaryId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int menuItemDietaryId)
        {
            await _menuItemDietaryService.Delete(menuItemDietaryId);
            return Ok();
        }
        #endregion

    }
}
