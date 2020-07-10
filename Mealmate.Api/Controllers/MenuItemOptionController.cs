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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuItemOptionController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMenuItemOptionService _menuItemOptionService;

        public MenuItemOptionController(
            IMediator mediator,
            IMenuItemOptionService menuItemOptionService
            )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _menuItemOptionService = menuItemOptionService ?? throw new ArgumentNullException(nameof(menuItemOptionService));
        }

        [Route("[action]")]
        [HttpGet("{optionItemId}")]
        [ProducesResponseType(typeof(IEnumerable<MenuItemOptionModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemOptionModel>>> GetMenuItemOptions(int menuItemId,int optionItemId)
        {
            var MenuItemOptions = await _menuItemOptionService.Get(menuItemId, optionItemId);

            return Ok(MenuItemOptions);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(MenuItemOptionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MenuItemOptionModel>> CreateMenuItemOption(CreateRequest<MenuItemOptionModel> request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> UpdateMenuItemOption(UpdateRequest<MenuItemOptionModel> request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> DeleteMenuItemOptionById(DeleteByIdRequest request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
        }

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
