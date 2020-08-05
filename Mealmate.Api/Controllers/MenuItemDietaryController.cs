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
    [Route("api/menuItemDietaries")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuItemDietaryController : ControllerBase
    {
        private readonly IMenuItemDietaryService _menuItemDietaryService;
        private readonly IDietaryService _dietaryService;

        public MenuItemDietaryController(
            IMenuItemDietaryService menuItemDietaryService,
            IDietaryService dietaryService
            )
        {
            _dietaryService = dietaryService ?? throw new ArgumentNullException(nameof(dietaryService));
            _menuItemDietaryService = menuItemDietaryService ?? throw new ArgumentNullException(nameof(menuItemDietaryService));
        }

        #region Read
        [Route("{menuItemId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<MenuItemDetailCreateDietaryModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemDetailCreateDietaryModel>>> List(int menuItemId)
        {
            try
            {
                List<MenuItemDetailCreateDietaryModel> model = new List<MenuItemDetailCreateDietaryModel>();

                var Options = await _menuItemDietaryService.Get(menuItemId);

                var temp = await _dietaryService.Get();

                foreach (var item in temp)
                {
                    model.Add(new MenuItemDetailCreateDietaryModel
                    {
                        MenuItemDietaryId = 0,
                        DietaryId = item.Id,
                        IsActive = false
                    });
                }

                foreach (var dietary in model)
                {
                    foreach (var item in Options)
                    {
                        if (dietary.DietaryId == item.DietaryId)
                        {
                            dietary.MenuItemDietaryId = item.Id;
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
        [ProducesResponseType(typeof(IEnumerable<MenuItemDietaryModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemDietaryModel>>> Get(
            int menuItemId, int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var MenuItemDietarys = await _menuItemDietaryService.Search(menuItemId, isActive, request);
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
                var temp = await _menuItemDietaryService.GetById(menuItemDietaryId);
                if (temp == null)
                {
                    return NotFound($"Resource with id {menuItemDietaryId} no more exists");
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
        [HttpPost]
        [ProducesResponseType(typeof(MenuItemDietaryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MenuItemDietaryModel>> Create([FromBody] MenuItemDietaryCreateModel request)
        {
            try
            {
                var result = await _menuItemDietaryService.Create(request);
                return Created($"api/menuitemdietaries/{result.Id}", result);
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
        public async Task<ActionResult> Update(int id, [FromBody] MenuItemDietaryUpdateModel request)
        {
            try
            {
                await _menuItemDietaryService.Update(id, request);
                return Ok();
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{menuItemDietaryId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int menuItemDietaryId)
        {
            try
            {
                await _menuItemDietaryService.Delete(menuItemDietaryId);
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
