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
    [Route("api/orderitemdetails")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemDetailController : ControllerBase
    {
        private readonly IOrderItemDetailService _orderItemDetailService;

        public OrderItemDetailController(IOrderItemDetailService orderitemdetailService)
        {
            _orderItemDetailService = orderitemdetailService ?? throw new ArgumentNullException(nameof(orderitemdetailService));
        }

        #region Read
        [HttpGet("{orderId}")]
        [ProducesResponseType(typeof(IEnumerable<OrderItemDetailModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderItemDetailModel>>> Get(int orderItemId, string props)
        {
            try
            {
                var result = await _orderItemDetailService.Get(orderItemId);
                JToken _jtoken = TokenService.CreateJToken(result, props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("single/{id}")]
        [ProducesResponseType(typeof(IEnumerable<OrderItemDetailModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderItemDetailModel>>> Get(int id)
        {
            var temp = await _orderItemDetailService.GetById(id);

            return Ok(temp);
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

            return BadRequest(ModelState);
        }
        #endregion

        #region Update
        [HttpPut()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(OrderItemDetailModel model)
        {
            //TODO: Add you code here
            try
            {
                if (ModelState.IsValid)
                {
                    await _orderItemDetailService.Update(model);
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
                await _orderItemDetailService.Delete(id);
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