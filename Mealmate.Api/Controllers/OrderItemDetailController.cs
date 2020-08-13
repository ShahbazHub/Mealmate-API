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
    [Route("api/orderitemdetails")]
    [ApiValidationFilter]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemDetailController : ControllerBase
    {
        private readonly IOrderItemDetailService _orderItemDetailService;

        public OrderItemDetailController(IOrderItemDetailService orderitemdetailService)
        {
            _orderItemDetailService = orderitemdetailService ?? throw new ArgumentNullException(nameof(orderitemdetailService));
        }

        #region Read
        [HttpGet("{orderItemId}")]
        [ProducesResponseType(typeof(IEnumerable<OrderItemDetailModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderItemDetailModel>>> Get(int orderItemId, string props)
        {
            try
            {
                var result = await _orderItemDetailService.Get(orderItemId);
                JToken _jtoken = TokenService.CreateJToken(result, props);
                return Ok(new ApiOkResponse(new { _jtoken }));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        [Route("single/{id}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<OrderItemDetailModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderItemDetailModel>>> Get(int id)
        {
            try
            {
                var temp = await _orderItemDetailService.GetById(id);
                if (temp == null)
                {
                    return NotFound(new ApiNotFoundResponse($"Resource with id {id} no more exists"));
                }
                 return Ok(new ApiOkResponse(new { temp }));
            }
            catch (System.Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }

        }
        #endregion

        #region Create
        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] OrderItemDetailModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _orderItemDetailService.Create(model);
                if (result != null)
                {
                    return Created($"api/orderitemdetails/{result.Id}", result);
                }
            }

             return BadRequest(new ApiBadRequestResponse(ModelState, $"Error while processing request"));;
        }
        #endregion

        #region Update
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, OrderItemDetailUpdateModel model)
        {
            //TODO: Add you code here
            try
            {
                if (ModelState.IsValid)
                {
                    await _orderItemDetailService.Update(id, model);
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
                await _orderItemDetailService.Delete(id);
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