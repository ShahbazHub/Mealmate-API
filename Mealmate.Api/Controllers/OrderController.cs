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
    [Route("api/orders")]
    [ApiValidationFilter]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        #region Read
        /// <summary>
        /// List all orders in a specific customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="props"></param>
        /// <returns></returns>
        [HttpGet("{customerId}")]
        [ProducesResponseType(typeof(IEnumerable<OrderModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderModel>>> Get(int customerId, string props)
        {
            try
            {
                var result = await _orderService.Get(customerId);
                JToken _jtoken = TokenService.CreateJToken(result, props);
                return Ok(new ApiOkResponse(new { _jtoken }));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        /// <summary>
        /// List all orders of a specific branch in specific state
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="orderStateId"></param>
        /// <returns></returns>
        [HttpGet("branch/{branchId}/state/{orderStateId}")]
        [ProducesResponseType(typeof(IEnumerable<OrderModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetByRestaurant(int branchId, int orderStateId)
        {
            try
            {
                var result = await _orderService.Get(branchId, orderStateId);
                 return Ok(new ApiOkResponse(new { result }));;
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        /// <summary>
        /// Get a specific order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("single/{id}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<OrderModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderModel>>> Get(int id)
        {
            try
            {
                var temp = await _orderService.GetById(id);
                if (temp == null)
                {
                    return NotFound(new ApiNotFoundResponse($"Resource with id {id} no more exists"));
                }

                 return Ok(new ApiOkResponse(new { temp }));
            }
            catch (Exception)
            {
                return BadRequest("Error while processing request");
            }
        }
        #endregion

        #region Create
        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] OrderCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderService.Create(model);
                if (result != null)
                {
                    return Created($"api/orders/{result.Id}", result);
                }
            }

             return BadRequest(new ApiBadRequestResponse(ModelState, $"Error while processing request"));;
        }
        #endregion

        #region Update
        /// <summary>
        /// Update an existing order
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, OrderUpdateModel model)
        {
            //TODO: Add you code here
            try
            {
                if (ModelState.IsValid)
                {
                    await _orderService.Update(id, model);
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
        /// <summary>
        /// Delete an existing order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _orderService.Delete(id);
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