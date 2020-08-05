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
    [Route("api/menuItemAllergens")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuItemAllergenController : ControllerBase
    {
        private readonly IMenuItemAllergenService _menuItemAllergenService;
        private readonly IAllergenService _allergenService;

        public MenuItemAllergenController(
            IMenuItemAllergenService menuItemAllergenService,
            IAllergenService allergenService
            )
        {
            _menuItemAllergenService = menuItemAllergenService ?? throw new ArgumentNullException(nameof(menuItemAllergenService));
            _allergenService = allergenService ?? throw new ArgumentNullException(nameof(allergenService));
        }

        #region Read
        [Route("{menuItemId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<MenuItemDetailCreateAllergenModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemDetailCreateAllergenModel>>> List(int menuItemId)
        {
            try
            {
                List<MenuItemDetailCreateAllergenModel> model = new List<MenuItemDetailCreateAllergenModel>();

                var Options = await _menuItemAllergenService.Get(menuItemId);

                var temp = await _allergenService.Get();

                foreach (var item in temp)
                {
                    model.Add(new MenuItemDetailCreateAllergenModel
                    {
                        MenuItemAllergenId = 0,
                        AllergenId = item.Id,
                        IsActive = false
                    });
                }

                foreach (var dietary in model)
                {
                    foreach (var item in Options)
                    {
                        if (dietary.AllergenId == item.AllergenId)
                        {
                            dietary.MenuItemAllergenId = item.Id;
                            dietary.IsActive = true;
                        }
                    }
                }
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Route("{menuItemId}/{isActive}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<MenuItemAllergenModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemAllergenModel>>> Get(
            int menuItemId, int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var MenuItemAllergens = await _menuItemAllergenService.Search(menuItemId, isActive, request);
                JToken _jtoken = TokenService.CreateJToken(MenuItemAllergens, request.Props);
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
                var temp = await _menuItemAllergenService.GetById(menuItemAllergenId);
                if (temp == null)
                {
                    return NotFound($"Resource with id {menuItemAllergenId} no more exists");
                }
                return Ok(temp);
            }
            catch (Exception)
            {
                return BadRequest("Error while processing your request");
            }
        }
        #endregion

        #region Create
        [HttpPost()]
        [ProducesResponseType(typeof(MenuItemAllergenModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MenuItemAllergenModel>> Create([FromBody] MenuItemAllergenCreateModel request)
        {
            try
            {
                var result = await _menuItemAllergenService.Create(request);
                return Created($"api/menuitemallergens/{result.Id}", result);
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
        public async Task<ActionResult> Update(int id, [FromBody] MenuItemAllergenUpdateModel request)
        {
            try
            {
                await _menuItemAllergenService.Update(id, request);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{menuItemAllergenId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int menuItemAllergenId)
        {
            try
            {
                await _menuItemAllergenService.Delete(menuItemAllergenId);
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
