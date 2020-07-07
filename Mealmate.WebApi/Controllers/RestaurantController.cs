using AutoMapper;
using Mealmate.BusinessLayer.Interface;
using Mealmate.DataAccess.Entities.Mealmate;
using Mealmate.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.WebApi.Controllers
{
    [Route("/api/restaurants")]
    [ApiController()]
    [Authorize()]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        public RestaurantController(IRestaurantService restaurantService, IMapper mapper)
        {
            _restaurantService = restaurantService;
            _mapper = mapper;
        }

        [HttpGet()]
        public ActionResult Get()
        {
            var result = _restaurantService.Get();

            return Ok(_mapper.Map<IEnumerable<RestaurantModel>>(result));
        }
    }
}
