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
    [Route("api/menuitemoptions")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        #region Read
        [Route("[action]")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<MenuItemOptionModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<MenuItemOptionModel>>> Get(int menuItemId,int optionItemId)
        {
            var MenuItemOptions = await _menuItemOptionService.Get(menuItemId, optionItemId);

            return Ok(MenuItemOptions);
        }
        #endregion

        #region Create
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(MenuItemOptionModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<MenuItemOptionModel>> Create(CreateRequest<MenuItemOptionModel> request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
        }
        #endregion

        #region Update
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(UpdateRequest<MenuItemOptionModel> request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
        }
        #endregion

        #region Delete
        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(DeleteByIdRequest request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
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
