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
    [Route("api/menuItemAllergens")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuItemAllergenController : ControllerBase
    {
        private readonly IMenuItemAllergenService _menuItemAllergenService;

        public MenuItemAllergenController(
            IMenuItemAllergenService menuItemAllergenService
            )
        {
            _menuItemAllergenService = menuItemAllergenService ?? throw new ArgumentNullException(nameof(menuItemAllergenService));
        }

        #region Read
        [Route("{menuItemId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<MenuItemAllergenModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemAllergenModel>>> Get(
            int menuItemId, [FromBody] SearchPageRequest request, string props)
        {
            try
            {
                var MenuItemAllergens = await _menuItemAllergenService.Search(menuItemId, request.Args);
                JToken _jtoken = TokenService.CreateJToken(MenuItemAllergens, props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("single/{menuItemAllergenId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(MenuItemAllergenModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MenuItemAllergenModel>> Get(int menuItemAllergenId)
        {
            try
            {
                var temp = await _menuItemAllergenService.Get(menuItemAllergenId);
                return Ok(temp);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Create
        [HttpPost()]
        [ProducesResponseType(typeof(MenuItemAllergenModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MenuItemAllergenModel>> Create([FromBody] MenuItemAllergenModel request)
        {
            var result = await _menuItemAllergenService.Create(request);
            return Created($"api/menuitemallergens/{result.Id}", result);
        }
        #endregion

        #region Update
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update([FromBody] MenuItemAllergenModel request)
        {
            await _menuItemAllergenService.Update(request);
            return Ok();
        }
        #endregion

        #region Delete
        [HttpDelete("{menuItemAllergenId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int menuItemAllergenId)
        {
            await _menuItemAllergenService.Delete(menuItemAllergenId);
            return Ok();
        }
        #endregion

    }
}
