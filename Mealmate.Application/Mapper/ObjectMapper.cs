using Mealmate.Application.Models;
using Mealmate.Core.Entities;
using AutoMapper;
using Mealmate.Core.Entities.Lookup;

namespace Mealmate.Application.Mapper
{
    public class MealMateMapper : Profile
    {
        public MealMateMapper()
        {

            CreateMap<UserAllergen, UserAllergenModel>().ReverseMap();
            CreateMap<UserDietary, UserDietaryModel>().ReverseMap();

            CreateMap<Restaurant, RestaurantModel>().ReverseMap();
            CreateMap<Branch, BranchModel>().ReverseMap();
            CreateMap<Location, LocationModel>().ReverseMap();
            CreateMap<Menu, MenuModel>().ReverseMap();
            CreateMap<MenuItem, MenuItemModel>().ReverseMap();
            CreateMap<MenuItemAllergen, MenuItemAllergenModel>().ReverseMap();
            CreateMap<MenuItemDietary, MenuItemDietaryModel>().ReverseMap();

            CreateMap<MenuItemOption, MenuItemOptionModel>().ReverseMap();
            CreateMap<QRCode, QRCodeModel>().ReverseMap();
            CreateMap<Table, TableModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<Allergen, AllergenModel>().ReverseMap();
            CreateMap<Dietary, DietaryModel>().ReverseMap();
            CreateMap<CuisineType, CuisineTypeModel>().ReverseMap();
        }


    }
}
