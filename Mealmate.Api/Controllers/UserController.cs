using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Configuration;
using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Mealmate.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly MealmateSettings _mealmateSettings;
        private readonly IUserService _userService;

        public UserController(
            IMediator mediator,
            IUserService userService,
            IOptions<MealmateSettings> options)
        {
            _mediator = mediator;
            _userService = userService;
            _mealmateSettings = options.Value;
        }


        #region Read
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<UserModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            var result = await _userService.Get();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserModel>> GetById(int id)
        {
            var result = await _userService.GetById(id);
            if (result == null)
            {
                return NotFound($"User with id {id} no more exists");
            }
            return Ok(result);
        }
        #endregion

        #region Register
        [HttpPost()]
        public ActionResult Register(CreateRequest<UserModel> request)
        {
            //TODO: Add you code here

            return Ok();
        }
        #endregion

        #region Update
        [HttpPost("{id}")]
        public async Task<ActionResult> Update(int id, UpdateRequest<UserModel> request)
        {
            //TODO: Add you code here
            var result = await _userService.GetById(id);
            if (result == null)
            {
                return NotFound($"User with id {id} no more exists");
            }

            return Ok();
        }
        #endregion

        #region Delete
        [HttpDelete()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(DeleteByIdRequest request)
        {
            var result = await _userService.GetById(request.Id);
            if (result == null)
            {
                return NotFound($"User with id {request.Id} no more exists");
            }

            return Ok();
        }
        #endregion
    }
}
