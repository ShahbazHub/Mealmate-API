using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Mealmate.Api.Helpers;
using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities.Lookup;
using Mealmate.Core.Paging;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Mealmate.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/orderstates")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderStateController : ControllerBase
    {
        private readonly IOrderStateService _orderStateService;

        public OrderStateController(IOrderStateService orderStateService)
        {
            _orderStateService = orderStateService ?? throw new ArgumentNullException(nameof(orderStateService));
        }

        #region Read
        [AllowAnonymous]
        [HttpGet("list")]
        [ProducesResponseType(typeof(IEnumerable<OrderStateModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderStateModel>>> Get()
        {
            try
            {
                var result = await _orderStateService.Get();
                if (result != null)
                {
                    result = result.Where(p => p.IsActive == true);
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("list/{isActive}")]
        [ProducesResponseType(typeof(IEnumerable<OrderStateModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderStateModel>>> Get(
            int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var result = await _orderStateService.Search(isActive, request);
                JToken _jtoken = TokenService.CreateJToken(result, request.Props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<OrderStateModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderStateModel>>> Get(int id)
        {
            try
            {
                var temp = await _orderStateService.GetById(id);
                if (temp == null)
                {
                    return NotFound($"Resource with id {id} no more exists");
                }

                return Ok(temp);
            }
            catch(Exception)
            {
                return BadRequest("Error while processing your request");
            }
        }
        #endregion

        #region Create
        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] OrderStateCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderStateService.Create(model);
                if (result != null)
                {
                    return Created($"api/orderStates/{result.Id}", result);
                }
            }

            return BadRequest(ModelState);
        }
        #endregion

        #region Update
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, OrderStateUpdateModel model)
        {
            //TODO: Add you code here
            try
            {
                if (ModelState.IsValid)
                {
                    await _orderStateService.Update(id, model);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _orderStateService.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
        #endregion
    }
}