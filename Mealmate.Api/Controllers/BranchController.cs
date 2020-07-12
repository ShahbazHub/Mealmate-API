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
    [Route("api/branches")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchController(
            IBranchService branchService
            )
        {
            _branchService = branchService;
        }

        #region Read
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BranchModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BranchModel>>> Get(int restaurantId)
        {
            var Branchs = await _branchService.Get(restaurantId);

            return Ok(Branchs);
        }
        #endregion

        #region Create
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(BranchModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<BranchModel>> Create(CreateRequest<BranchModel> request)
        {
            var commandResult = await _branchService.Create(request.Model);

            return Ok(commandResult);
        }
        #endregion

        #region Update
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(UpdateRequest<BranchModel> request)
        {
            await _branchService.Update(request.Model);
            return Ok();
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete branch by id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(DeleteByIdRequest request)
        {
            try
            {
                await _branchService.Delete(request.Id);
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
