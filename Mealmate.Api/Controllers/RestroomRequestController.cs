using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Mealmate.Api.Application.Firebase;
using Mealmate.Api.Helpers;
using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Entities;
using Mealmate.Core.Entities.Lookup;
using Mealmate.Core.Paging;
using Mealmate.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Mealmate.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/restroomrequests")]
    [ApiValidationFilter]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RestroomRequestController : ControllerBase
    {
        private readonly IRestroomRequestService _restroomRequestService;
        private readonly UserManager<User> _userManager;
        private readonly ITableService _tableService;
        private readonly IFCMService _fCMService;
        private readonly MealmateContext _mealmateContext;

        public RestroomRequestController(
            UserManager<User> userManager,
            IRestroomRequestService restroomRequestService,
            ITableService tableService,
            IFCMService fCMService,
            MealmateContext mealmateContext)
        {
            _userManager = userManager;
            _tableService = tableService ?? throw new ArgumentNullException(nameof(tableService));
            _fCMService = fCMService;
            _mealmateContext = mealmateContext;
            _restroomRequestService = restroomRequestService ?? throw new ArgumentNullException(nameof(restroomRequestService));
        }

        #region Read
        /// <summary>
        /// Get list by branch and request state
        /// </summary>
        /// <param name="branchId"></param>
        /// <param name="restroomRequestStateId"></param>
        /// <returns></returns>
        [HttpGet("list/{branchId}/state/{restroomRequestStateId}")]
        [ProducesResponseType(typeof(IEnumerable<RestroomRequestModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RestroomRequestModel>>> Get(
            int branchId, int restroomRequestStateId)
        {
            try
            {
                var result = await _restroomRequestService.Get(branchId, restroomRequestStateId);
                return Ok(new ApiOkResponse(result)); ;
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        /// <summary>
        /// List all restroom requests of a specific customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpGet("list/{customerId}")]
        [ProducesResponseType(typeof(IEnumerable<RestroomRequestModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RestroomRequestModel>>> Get(
            int customerId, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var result = await _restroomRequestService.Search(customerId, request);
                JToken _jtoken = TokenService.CreateJToken(result, request.Props);
                return Ok(new ApiOkResponse(_jtoken));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }

        /// <summary>
        /// Get a specific restroom request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<RestroomRequestModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<RestroomRequestModel>>> Get(int id)
        {
            try
            {
                var temp = await _restroomRequestService.GetById(id);
                if (temp == null)
                {
                    return NotFound(new ApiNotFoundResponse($"Resource with id {id} no more exists"));
                }

                return Ok(new ApiOkResponse(temp));
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request")); ;
            }
        }
        #endregion

        #region Create
        /// <summary>
        /// Create a new restroom request
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] RestroomRequestCreateModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = await _userManager.Users.FirstOrDefaultAsync(p => p.Id == model.CustomerId);
                if (customer == null)
                {
                    return NotFound(new ApiNotFoundResponse($"User does't exist"));
                }

                var table = await _tableService.GetById(model.TableId);
                if (table == null)
                {
                    return NotFound(new ApiNotFoundResponse($"Table doesn't exists"));
                }

                var result = await _restroomRequestService.Create(model);
                if (result != null)
                {
                    //Todo: find the relevant FrontDesk User to send push notification
                    var tableResult = _mealmateContext.Tables.First(x => x.Id == model.TableId);
                    var locationId = table.LocationId;
                    var branchId = _mealmateContext.Locaations.First(x => x.Id == locationId)?.BranchId;

                    var RegistrationTokens = _mealmateContext
                                            .UserRoles.Join(_mealmateContext.UserBranches,
                                             userRole => userRole.UserId,
                                             userbranch => userbranch.UserId,
                                             (ur, ub) => new { ur, ub })
                                            .Join(_mealmateContext.FCMRegistrationTokens,
                                             outer => outer.ur.UserId,
                                             fcmRT => fcmRT.UserId,
                                             (outer, fcmRT) => new { outer, fcmRT }
                                            )
                                            .Where(x => x.outer.ub.BranchId == branchId && x.outer.ur.Role.Name == ApplicationRoles.FrontDesk)
                                            .Select(x => x.fcmRT.RegistrationToken).ToList<string>();

                    if (RegistrationTokens.Count > 0)
                        await _fCMService.SendMulticastAsync(RegistrationTokens,
                            new FirebaseAdmin.Messaging.Notification
                            {
                                Title = "New RestRoom Request",
                                Body = $"Table {tableResult.Name} requested for Rest Room",
                                ImageUrl = "https://localhost/images/abc.jpg"
                            });


                    return Created($"api/restroomRequests/{result.Id}", result);
                }
            }

            return BadRequest(new ApiBadRequestResponse(ModelState, $"Error while processing request")); ;
        }
        #endregion

        #region Update
        /// <summary>
        /// Update an existing restroom request
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, [FromBody] RestroomRequestUpdateModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _restroomRequestService.Update(id, model);

                    if (model.RestroomRequestStateId == 2)
                    {
                        var ClientToken = _mealmateContext
                                            .FCMRegistrationTokens
                                            .Join(_mealmateContext.RestroomRequests,
                                            fcmRT => fcmRT.UserId,
                                            RRR => RRR.CustomerId,
                                            (fcm, rrrn) => new { fcm, rrrn })
                                            .Where(x => x.rrrn.Id == id)
                                            .Select(x => x.fcm.RegistrationToken).First<string>();

                        await _fCMService.SendToTokenAsync(ClientToken, new FirebaseAdmin.Messaging.Notification
                        {
                            Title = "Alert",
                            Body = $"You RestRoom Rest has been accepted",
                            ImageUrl = "https://localhost/images/abc.jpg"
                        });
                    }
                }
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request")); ;
            }

            return Ok(new ApiOkResponse());
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete an existing restroom request
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
                await _restroomRequestService.Delete(id);
            }
            catch (Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request")); ;
            }

            return Ok(new ApiOkResponse($"Deleted"));
        }
        #endregion
    }
}