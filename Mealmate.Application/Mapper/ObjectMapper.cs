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
                cfg.CreateMap<Branch, BranchModel>().ReverseMap();
                cfg.CreateMap<Location, LocationModel>().ReverseMap();
                cfg.CreateMap<Menu, MenuModel>().ReverseMap();
                cfg.CreateMap<MenuItem, MenuItemModel>().ReverseMap();
                cfg.CreateMap<MenuItemOption, MenuItemOptionModel>().ReverseMap();
                cfg.CreateMap<QRCode, QRCodeModel>().ReverseMap();
                cfg.CreateMap<Table, TableModel>().ReverseMap();
                cfg.CreateMap<User, UserModel>().ReverseMap();
            });
        }
    }
}
