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
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Mealmate.Api.Controllers
{
    [Route("api/branches")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        #region Read
        [HttpGet]
        [Route("{restaurantId}/{isActive}")]
        [ProducesResponseType(typeof(IEnumerable<BranchModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BranchModel>>> Get(
            int restaurantId, int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var Branches = await _branchService.Search(restaurantId, isActive, request);
                JToken _jtoken = TokenService.CreateJToken(Branches, request.Props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("single/{branchId}")]
        [ProducesResponseType(typeof(IEnumerable<BranchModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BranchModel>>> Get(int branchId)
        {
            try
            {
                var Branch = await _branchService.GetById(branchId);
                return Ok(Branch);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Create
        [HttpPost]
        [ProducesResponseType(typeof(BranchModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<BranchModel>> Create(BranchCreateModel request)
        {
            var commandResult = await _branchService.Create(request);

            return Ok(commandResult);
        }
        #endregion

        #region Update
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, BranchUpdateModel request)
        {
            await _branchService.Update(id, request);
            return Ok();
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete branch by id
        /// </summary>
        /// <param name="branchId"></param>
        /// <returns></returns>
        [HttpDelete("{branchId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int branchId)
        {
            try
            {
                await _branchService.Delete(branchId);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        #endregion
    }
}
