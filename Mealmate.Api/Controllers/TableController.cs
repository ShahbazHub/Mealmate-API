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
    [Route("api/tables")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(
            ITableService tableService
            )
        {
            _tableService = tableService ?? throw new ArgumentNullException(nameof(tableService));
        }

        #region Read

        [HttpGet]
        [Route("{locationId}/{isActive}")]
        [ProducesResponseType(typeof(IEnumerable<TableModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<TableModel>>> Get(
            int locationId, int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var Tables = await _tableService.Search(locationId, isActive, request);
                JToken _jtoken = TokenService.CreateJToken(Tables, request.Props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("single/{tableId}")]
        [ProducesResponseType(typeof(IEnumerable<TableModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<TableModel>>> Get(int tableId)
        {
            try
            {
                var Tables = await _tableService.GetById(tableId);
                return Ok(Tables);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Create
        [HttpPost]
        [ProducesResponseType(typeof(TableModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TableModel>> Create(TableCreateModel request)
        {
            try
            {
                var result = await _tableService.Create(request);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }

        }
        #endregion

        #region Update
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(TableUpdateModel request)
        {
            try
            {
                await _tableService.Update(request);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{tableId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int tableId)
        {
            try
            {
                await _tableService.Delete(tableId);
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
