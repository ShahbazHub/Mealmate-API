using AutoMapper;
using Mealmate.DataAccess.Entities.Mealmate;
using Mealmate.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mealmate.WebApi.Profiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantModel>();
        }
    }
}
