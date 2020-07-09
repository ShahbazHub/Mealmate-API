using Mealmate.Application.Models;
using Mealmate.Core.Entities;
using AutoMapper;

namespace Mealmate.Application.Mapper
{
    public class ObjectMapper
    {
        public static IMapper Mapper => AutoMapper.Mapper.Instance;

        private static void CreateMap()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Restaurant, RestaurantModel>().ReverseMap();
            });
        }
    }
}
