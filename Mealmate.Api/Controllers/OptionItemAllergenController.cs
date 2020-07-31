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
    [Route("api/optionItemAllergens")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OptionItemAllergenController : ControllerBase
    {
        private readonly IOptionItemAllergenService _optionItemAllergenService;
        private readonly IAllergenService _allergenService;

        public OptionItemAllergenController(
            IOptionItemAllergenService optionItemAllergenService,
            IAllergenService allergenService
            )
        {
            _optionItemAllergenService = optionItemAllergenService ?? throw new ArgumentNullException(nameof(optionItemAllergenService));
            _allergenService = allergenService ?? throw new ArgumentNullException(nameof(allergenService));
        }

        #region Read
        [Route("{optionItemId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<OptionItemDetailCreateAllergenModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OptionItemDetailCreateAllergenModel>>> List(int optionItemId)
        {
            try
            {
                List<OptionItemDetailCreateAllergenModel> model = new List<OptionItemDetailCreateAllergenModel>();

                var Options = await _optionItemAllergenService.Get(optionItemId);

                var temp = await _allergenService.Get();

                foreach (var item in temp)
                {
                    model.Add(new OptionItemDetailCreateAllergenModel
                    {
                        OptionItemAllergenId = 0,
                        AllergenId = item.Id,
                        IsActive = false
                    });
                }

                foreach (var dietary in model)
                {
                    foreach (var item in Options)
                    {
                        if (dietary.AllergenId == item.AllergenId)
                        {
                            dietary.OptionItemAllergenId = item.Id;
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
        [ProducesResponseType(typeof(IEnumerable<OptionItemAllergenModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OptionItemAllergenModel>>> Get(
            int optionItemId, int isActive, [FromQuery] PageSearchArgs request)
        {
            try
            {
                var OptionItemAllergens = await _optionItemAllergenService.Search(optionItemId, isActive, request);
                JToken _jtoken = TokenService.CreateJToken(OptionItemAllergens, request.Props);
                return Ok(_jtoken);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Route("single/{optionItemAllergenId}")]
        [HttpGet()]
        [ProducesResponseType(typeof(OptionItemAllergenModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OptionItemAllergenModel>> Get(int optionItemAllergenId)
        {
            try
            {
                var temp = await _optionItemAllergenService.GetById(optionItemAllergenId);
                if (temp == null)
                {
                    return NotFound($"Resource with id {optionItemAllergenId} no more exists");
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
        [ProducesResponseType(typeof(OptionItemAllergenModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<OptionItemAllergenModel>> Create([FromBody] OptionItemAllergenCreateModel request)
        {
            try
            {
                var result = await _optionItemAllergenService.Create(request);
                return Created($"api/optionitemallergens/{result.Id}", result);
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
        public async Task<ActionResult> Update(int id, [FromBody] OptionItemAllergenUpdateModel request)
        {
            try
            {
                await _optionItemAllergenService.Update(id, request);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{optionItemAllergenId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int optionItemAllergenId)
        {
            try
            {
                await _optionItemAllergenService.Delete(optionItemAllergenId);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        #endregion

    }
}
