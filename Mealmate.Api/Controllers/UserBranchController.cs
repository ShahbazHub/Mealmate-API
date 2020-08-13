using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mealmate.Api.Controllers
{
    [ApiValidationFilter]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/userbranches")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserBranchController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRestaurantService _userRestaurantService;
        private readonly IUserBranchService _userBranchService;
        private readonly IRestaurantService _restaurantService;
        
        public UserBranchController(
                IUserService userService,
                IUserRestaurantService userRestaurantService,
                IRestaurantService restaurantService,
                IUserBranchService userBranchService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _userRestaurantService = userRestaurantService ?? throw new ArgumentNullException(nameof(userRestaurantService));
            _restaurantService = restaurantService ?? throw new ArgumentNullException(nameof(restaurantService));
            _userBranchService = userBranchService ?? throw new ArgumentNullException(nameof(userBranchService));
        }

        /// <summary>
        /// Get all branches for the user in a specific restaurant
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("restaurant/{restaurantId}/user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<BranchModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get(int restaurantId, int userId)
        {
            try
            {
                var result = await _userBranchService.Get(restaurantId, userId);
                 return Ok(new ApiOkResponse(new { result }));;

            }
            catch (System.Exception)
            {
                return BadRequest(new ApiBadRequestResponse($"Error while processing request"));
            }
        }
    }
}
