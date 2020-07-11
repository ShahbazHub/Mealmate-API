using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities;
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
    [Route("api/qrcodes")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class QRCodeController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IQRCodeService _qRCodeService;

        public QRCodeController(
            IMediator mediator,
            IQRCodeService qRCodeService
            )
        {
            _mediator = mediator;
            _qRCodeService = qRCodeService;
        }

        #region Read
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<QRCodeModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<QRCodeModel>>> Get(int tableId)
        {
            var result = await _qRCodeService.Get(tableId);

            return Ok(result);
        }
        #endregion

        #region Create
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(QRCodeModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<QRCodeModel>> Create(CreateRequest<QRCodeModel> request)
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
        public async Task<ActionResult> Update(UpdateRequest<QRCodeModel> request)
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
        //[ProducesResponseType(typeof(IPagedList<QRCodeModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IPagedList<QRCodeModel>>> SearchQRCodes(SearchPageRequest request)
        //{
        //    var QRCodePagedList = await _restaurantService.SearchQRCodes(request.Args);

        //    return Ok(QRCodePagedList);
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(QRCodeModel), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<QRCodeModel>> GetQRCodeById(GetQRCodeByIdRequest request)
        //{
        //    var QRCode = await _restaurantService.GetQRCodeById(request.Id);

        //    return Ok(QRCode);
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(IEnumerable<QRCodeModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<QRCodeModel>>> GetQRCodesByName(GetResturantsByNameRequest request)
        //{
        //    var QRCodes = await _restaurantService.GetQRCodesByName(request.Name);

        //    return Ok(QRCodes);
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(IEnumerable<QRCodeModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<QRCodeModel>>> GetQRCodesByCategoryId(GetQRCodesByCategoryIdRequest request)
        //{
        //    var QRCodes = await _restaurantService.GetQRCodesByCategoryId(request.CategoryId);

        //    return Ok(QRCodes);
        //}
    }
}
