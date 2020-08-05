using Mealmate.Api.Helpers;
using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Paging;


using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Mealmate.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Route("api/optionItemDietaries")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OptionItemDietaryController : ControllerBase
    {
        private readonly IOptionItemDietaryService _optionItemDietaryService;
        private readonly IDietaryService _dietaryService;

        public OptionItemDietaryController(
            IOptionItemDietaryService optionItemDietaryService,
            IDietaryService dietaryService
            )
        {
            _dietaryService = dietaryService ?? throw new ArgumentNullException(nameof(dietaryService));
            _optionItemDietaryService = optionItemDietaryService ?? throw new ArgumentNullException(nameof(optionItemDietaryService));
        }

        #region Read
        [Route("{optionItemId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<OptionItemDetailCreateDietaryModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OptionItemDetailCreateDietaryModel>>> List(int optionItemId)
        {
            try
            {
                List<OptionItemDetailCreateDietaryModel> model = new List<OptionItemDetailCreateDietaryModel>();

                var OptionItemDietarys = await _optionItemDietaryService.Get(optionItemId);

                var temp = await _dietaryService.Get();

                foreach (var item in temp)
                {
                    model.Add(new OptionItemDetailCreateDietaryModel
                    {
                        OptionItemDietaryId = 0,
                        DietaryId = item.Id,
                        IsActive = false
                    });
                }

                foreach (var dietary in model)
                {
                    foreach (var item in OptionItemDietarys)
                    {
                        if (dietary.DietaryId == item.DietaryId)
                        {
                            dietary.OptionItemDietaryId = item.Id;
                            dietary.IsActive = true;
                        }
                    }
                }
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [Route("{optionItemId}/{isActive}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<OptionItemDietaryModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OptionItemDietaryModel>>> Get(
            int optionItemId, int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var OptionItemDietarys = await _optionItemDietaryService.Search(optionItemId, isActive, request);
                JToken _jtoken = TokenService.CreateJToken(OptionItemDietarys, request.Props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("single/{optionItemDietaryId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(OptionItemDietaryModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OptionItemDietaryModel>> Get(int optionItemDietaryId)
        {
            try
            {
                var temp = await _optionItemDietaryService.GetById(optionItemDietaryId);
                if (temp == null)
                {
                    return NotFound($"Resource with id {optionItemDietaryId} no more exists");
                }
                return Ok(temp);
            }
            catch (Exception)
            {
                return BadRequest("Error while processing your request");
            }
        }
        #endregion

        #region Create
        [HttpPost()]
        [ProducesResponseType(typeof(OptionItemDietaryModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<OptionItemDietaryModel>> Create([FromBody] OptionItemDietaryCreateModel request)
        {
            try
            {
                var result = await _optionItemDietaryService.Create(request);
                return Created($"api/optionitemdietaries/{result.Id}", result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Update
        [HttpPost("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(int id, [FromBody] OptionItemDietaryUpdateModel request)
        {
            try
            {
                await _optionItemDietaryService.Update(id, request);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{optionItemDietaryId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int optionItemDietaryId)
        {
            await _optionItemDietaryService.Delete(optionItemDietaryId);
            return Ok();
        }
        #endregion

    }
}
