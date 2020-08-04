using Mealmate.Api.Helpers;
using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities;
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
    [Route("api/qrcodes")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class QRCodeController : ControllerBase
    {
        private readonly IQRCodeService _qRCodeService;

        public QRCodeController(
            IQRCodeService qRCodeService
            )
        {
            _qRCodeService = qRCodeService;
        }

        #region Read
        [HttpGet("{tableId}")]
        [ProducesResponseType(typeof(IEnumerable<QRCodeModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<QRCodeModel>>> Get(int tableId, string props)
        {
            try
            {
                var result = await _qRCodeService.Get(tableId);
                JToken _jtoken = TokenService.CreateJToken(result, props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("single/{qrCodeId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(QRCodeModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<QRCodeModel>> Get(int qrCodeId)
        {
            try
            {
                var temp = await _qRCodeService.GetById(qrCodeId);
                return Ok(temp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Create
        [HttpPost]
        [ProducesResponseType(typeof(QRCodeModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<QRCodeModel>> Create([FromBody] QRCodeCreateModel request)
        {
            var commandResult = await _qRCodeService.Create(request);
            return Ok(commandResult);
        }
        #endregion

        #region Update
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(QRCodeModel request)
        {
            await _qRCodeService.Update(request);
            return Ok();
        }
        #endregion

        #region Delete
        [HttpDelete("{qrCodeId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int qrCodeId)
        {
            await _qRCodeService.Delete(qrCodeId);
            return Ok();
        }
        #endregion


    }
}
