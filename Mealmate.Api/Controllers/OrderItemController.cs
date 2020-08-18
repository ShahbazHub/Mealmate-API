using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Mealmate.Api.Helpers;
using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities.Lookup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Mealmate.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/orderitems")]
    [ApiValidationFilter]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderitemService;

        public OrderItemController(IOrderItemService orderitemService)
        {
            _orderitemService = orderitemService ?? throw new ArgumentNullException(nameof(orderitemService));
        }

        #region Read
        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(IEnumerable<OrderItemModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderItemModel>>> Get(int orderId, string props)
        {
            try
            {
                var result = await _orderitemService.Get(orderId);
                JToken _jtoken = TokenService.CreateJToken(result, props);
                return Ok(new ApiOkResponse(_jtoken));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        [Route("single/{id}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<OrderItemModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderItemModel>>> Get(int id)
        {
            try
            {
                var temp = await _orderitemService.GetById(id);
                if (temp == null)
                {
                    return NotFound(new ApiNotFoundResponse($"Resource with id {id} no more exists"));
                }
                 return Ok(new ApiOkResponse(temp));
            }
            catch (System.Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }
        #endregion

        #region Create
        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] OrderItemCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderitemService.Create(model);
                if (result != null)
                {
                    return Created($"api/orderitems/{result.Id}", result);
                }
            }

             return BadRequest(new ApiBadRequestResponse(ModelState, $"Error while processing request"));;
        }
        #endregion

        #region Update
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, [FromBody] OrderItemUpdateModel model)
        {
            //TODO: Add you code here
            try
            {
                if (ModelState.IsValid)
                {
                    await _orderitemService.Update(id, model);
                }
            }
            catch (Exception )
            {
                 return BadRequest(new ApiBadRequestResponse($"Error while processing request"));;
            }

             return Ok(new ApiOkResponse());
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
                await _orderitemService.Delete(id);
            }
            catch (Exception )
            {
                 return BadRequest(new ApiBadRequestResponse($"Error while processing request"));;
            }

             return Ok(new ApiOkResponse($"Deleted"));
        }
        #endregion
    }
}