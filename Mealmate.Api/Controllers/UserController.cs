﻿using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Configuration;
using Mealmate.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mealmate.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
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
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            var result = await _userService.Get();
            return Ok(result);
        }

        [Route("[action]")]
        [HttpGet]
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
        [HttpPost("register")]
        public ActionResult Register(CreateRequest<RestaurantModel> request)
        {
            //TODO: Add you code here
            return Ok();
        }
        #endregion

        #region Update
        [HttpPost("register")]
        public ActionResult Update(UpdateRequest<UserModel> request)
        {
            //TODO: Add you code here
            return Ok();
        }
        #endregion

        #region Delete
        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public ActionResult Delete(DeleteByIdRequest request)
        {
            return Ok();
        }
        #endregion
    }
}
