using Mealmate.Api.Helpers;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Configuration;
using Mealmate.Core.Entities;
using Mealmate.Core.Paging;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Mealmate.Api.Controllers
{
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/users")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserRestaurantService _userRestaurantService;
        private readonly IRestaurantService _restaurantService;

        public UserController(
                IUserService userService,
                UserManager<User> userManager,
                RoleManager<Role> roleManager,
                IUserRestaurantService userRestaurantService,
                IRestaurantService restaurantService
            )
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _userRestaurantService = userRestaurantService ?? throw new ArgumentNullException(nameof(userRestaurantService));
            _restaurantService = restaurantService ?? throw new ArgumentNullException(nameof(restaurantService));

            _userManager = userManager;
            _roleManager = roleManager;
        }

        #region Read
        /// <summary>
        /// Get list of users in mealmate
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<UserModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            try
            {
                var result = await _userService.Get();
                return Ok(result);

            }
            catch (System.Exception)
            {
                return BadRequest($"Error processing your request");
            }
        }

        /// <summary>
        /// Get a specific user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<UserModel>> GetById(int id)
        {
            try
            {
                var result = await _userService.GetById(id);
                if (result == null)
                {
                    return NotFound($"User with id {id} no more exists");
                }
                return Ok(result);
            }
            catch (System.Exception)
            {
                return BadRequest($"Error processing your request");
            }
        }

        /// <summary>
        /// Get list of all users in a specific restaurant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("restaurant/{id}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<UserModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserModel>>> List(int id, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var result = await _userRestaurantService.List(id, request);
                JToken _jtoken = TokenService.CreateJToken(result, request.Props);
                return Ok(_jtoken);
            }
            catch (System.Exception)
            {
                return BadRequest($"Error processing your request");
            }
        }
        #endregion

        #region Create / Regiaster
        /// <summary>
        /// Create a new user for a specific restaurant
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<ActionResult> Create([FromBody] UserCreateModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userService.Create(model);
                    if (user != null)
                    {
                        return Created($"/api/users/{user.Id}", user);
                    }
                }

                return BadRequest($"Error registering new user");
            }
            catch (System.Exception)
            {
                return BadRequest($"Error processing your request");
            }
        }

        #endregion

        #region Update
        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UserUpdateModel request)
        {
            try
            {
                var result = await _userService.GetById(id);
                if (result == null)
                {
                    return NotFound($"User with id {id} no more exists");
                }
                var updatedUser = await _userService.Update(id, request);

                return Ok(updatedUser);
            }
            catch (System.Exception)
            {
                return BadRequest("Error processing your request");
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete an existing user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _userService.Delete(id);
                return NoContent();
            }
            catch (ApplicationException ex)
            {
                return BadRequest($"{ex.Message}");
            }
            catch (Exception)
            {
                return BadRequest("Error processing your request");
            }
        }
        #endregion
    }
}
