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
    [ApiValidationFilter]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<MenuItemModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemModel>>> Get(
           [FromQuery] BranchSearchModel model, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var MenuItems = await _menuItemService.Search(model, request);
                JToken _jtoken = TokenService.CreateJToken(MenuItems, request.Props);
                return Ok(new ApiOkResponse(_jtoken));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

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
                return Ok(new ApiOkResponse(_jtoken));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        [Route("menuitemslazy/{menuId}/{isActive}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<MenuItemModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemModel>>> GetLazy(
            int menuId, int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var MenuItems = await _menuItemService.SearchLazy(menuId, isActive, request);
                JToken _jtoken = TokenService.CreateJToken(MenuItems, request.Props);
                return Ok(new ApiOkResponse(_jtoken));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
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
                    return NotFound(new ApiNotFoundResponse($"Resource with id {menuItemId} no more exists"));
                }
                 return Ok(new ApiOkResponse(temp));
            }
            catch (Exception)
            {
                 return BadRequest(new ApiBadRequestResponse($"Error while processing request"));;
            }
        }

        [Route("AddToCart/{menuItemId}")]
        [AllowAnonymous]
        [HttpGet()]
        [ProducesResponseType(typeof(OrderItemModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderItemModel>> AddToCart(int menuItemId)
        {
            try
            {
                var temp = await _menuItemService.AddToCart(menuItemId);
                if (temp == null)
                {
                    return NotFound(new ApiNotFoundResponse($"Resource with id {menuItemId} no more exists"));
                }
                return Ok(new ApiOkResponse(temp));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request")); ;
            }
        }


        

        [Route("filter")]
        [HttpPost()]
        [ProducesResponseType(typeof(IEnumerable<MenuItemModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemModel>>> Get(
            [FromQuery] MenuFilterRequest filterRequest)
        {
            try
            {
                //TODO: filtering the menu items having allergens / dietaries
                var MenuItems = await _menuItemService.Get(filterRequest.allergenIds, filterRequest.dietaryIds);
                return Ok(MenuItems);
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
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
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
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
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }
        #endregion

        #region Update
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, [FromBody] MenuItemUpdateModel request)
        {
            try
            {
                await _menuItemService.Update(id, request);
                 return Ok(new ApiOkResponse());
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        [Route("bulk/{id}")]
        [HttpPost()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, [FromBody] MenuItemDetailUpdateModel model)
        {
            //TODO: Add you code here
            try
            {
                if (ModelState.IsValid)
                {
                    await _menuItemService.Update(id, model);
                }
            }
            catch (Exception )
            {
                 return BadRequest(new ApiBadRequestResponse($"Error while processing request"));;
            }

             return Ok(new ApiOkResponse());
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
                 return Ok(new ApiOkResponse());
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }
        #endregion

    }
}
