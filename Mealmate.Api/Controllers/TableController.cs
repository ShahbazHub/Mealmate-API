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
    [Route("api/tables")]
    [ApiValidationFilter]
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
                return Ok(new ApiOkResponse(new { _jtoken }));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        [HttpGet]
        [Route("single/{tableId}")]
        [ProducesResponseType(typeof(IEnumerable<TableModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<TableModel>>> Get(int tableId)
        {
            try
            {
                var table = await _tableService.GetById(tableId);
                if (table == null)
                {
                    return NotFound(new ApiNotFoundResponse($"Resource with id {tableId} no more exists"));
                }
                return Ok(table);
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
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
                 return Ok(new ApiOkResponse(new { result }));;
            }
            catch (System.Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }

        }

        [Route("bulk")]
        [HttpPost()]
        [ProducesResponseType(typeof(TableModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TableModel>> Create(TableBulkCreateModel request)
        {
            try
            {
                var result = await _tableService.Create(request);
                 return Ok(new ApiOkResponse(new { result }));;
            }
            catch (System.Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }

        }
        #endregion

        #region Update
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, TableUpdateModel request)
        {
            try
            {
                await _tableService.Update(id, request);
                 return Ok(new ApiOkResponse());
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
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
