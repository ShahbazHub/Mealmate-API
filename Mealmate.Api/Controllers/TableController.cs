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
        [Route("{branchId}")]
        [ProducesResponseType(typeof(IEnumerable<TableModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<TableModel>>> Get(int branchId, string props)
        {
            try
            {
                var Tables = await _tableService.Get(branchId);
                JToken _jtoken = TokenService.CreateJToken(Tables, props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Create
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(TableModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<TableModel>> Create(TableModel request)
        {
            var result = await _tableService.Create(request);
            return Ok(result);
        }
        #endregion

        #region Update
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(TableModel request)
        {
            await _tableService.Update(request);
            return Ok();
        }
        #endregion

        #region Delete
        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int tableId)
        {
            await _tableService.Delete(tableId);
            return Ok();
        }
        #endregion


    }
}
