using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Paging;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Mealmate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuItemController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(
            IMediator mediator,
            IMenuItemService menuItemService
            )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _menuItemService = menuItemService ?? throw new ArgumentNullException(nameof(menuItemService));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MenuItemModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemModel>>> GetMenuItems(int ownerId)
        {
            var MenuItems = await _menuItemService.Get(ownerId);

            return Ok(MenuItems);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(MenuItemModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MenuItemModel>> CreateMenuItem(CreateRequest<MenuItemModel> request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> UpdateMenuItem(UpdateRequest<MenuItemModel> request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> DeleteMenuItemById(DeleteByIdRequest request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
        }

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
