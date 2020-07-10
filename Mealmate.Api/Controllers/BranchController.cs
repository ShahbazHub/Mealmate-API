using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;
using Mealmate.Core.Paging;

using MediatR;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Mealmate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BranchController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IBranchService _branchService;

        public BranchController(
            IMediator mediator,
            IBranchService branchService
            )
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _branchService = branchService ?? throw new ArgumentNullException(nameof(branchService));
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BranchModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<BranchModel>>> GetBranchs(int ownerId)
        {
            var Branchs = await _branchService.Get(ownerId);

            return Ok(Branchs);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(typeof(BranchModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<BranchModel>> CreateBranch(CreateRequest<BranchModel> request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> UpdateBranch(UpdateRequest<BranchModel> request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> DeleteBranchById(DeleteByIdRequest request)
        {
            var commandResult = await _mediator.Send(request);

            return Ok(commandResult);
        }

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(IPagedList<BranchModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IPagedList<BranchModel>>> SearchBranchs(SearchPageRequest request)
        //{
        //    var BranchPagedList = await _branchService.SearchBranchs(request.Args);

        //    return Ok(BranchPagedList);
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(BranchModel), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<BranchModel>> GetBranchById(GetBranchByIdRequest request)
        //{
        //    var Branch = await _branchService.GetBranchById(request.Id);

        //    return Ok(Branch);
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(IEnumerable<BranchModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<BranchModel>>> GetBranchsByName(GetResturantsByNameRequest request)
        //{
        //    var Branchs = await _branchService.GetBranchsByName(request.Name);

        //    return Ok(Branchs);
        //}

        //[Route("[action]")]
        //[HttpPost]
        //[ProducesResponseType(typeof(IEnumerable<BranchModel>), (int)HttpStatusCode.OK)]
        //public async Task<ActionResult<IEnumerable<BranchModel>>> GetBranchsByCategoryId(GetBranchsByCategoryIdRequest request)
        //{
        //    var Branchs = await _branchService.GetBranchsByCategoryId(request.CategoryId);

        //    return Ok(Branchs);
        //}
    }
}
