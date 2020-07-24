using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Paging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IUserService _userService;

        public UserController(
            IUserService userService
            )
        {
            _userService = userService;
        }


        #region Read
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<UserModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get([FromQuery] PageSearchArgs request)
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

        #region Update
        [HttpPost("{id}")]
        public async Task<ActionResult> Update(UserModel request)
        {
            //TODO: Add you code here
            var result = await _userService.GetById(request.Id);
            if (result == null)
            {
                return NotFound($"User with id {request.Id} no more exists");
            }

            return Ok();
        }
        #endregion

        #region Delete
        [HttpDelete()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(int userId)
        {
            var result = await _userService.GetById(userId);
            if (result == null)
            {
                return NotFound($"User with id {userId} no more exists");
            }

            return Ok();
        }
        #endregion
    }
}
