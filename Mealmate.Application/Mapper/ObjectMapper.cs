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

            // Identity
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<UserAllergen, UserAllergenModel>().ReverseMap();
            CreateMap<UserDietary, UserDietaryModel>().ReverseMap();

            // Mealmate
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

            // Lookup
            CreateMap<Allergen, AllergenModel>().ReverseMap();
            CreateMap<Dietary, DietaryModel>().ReverseMap();
            CreateMap<CuisineType, CuisineTypeModel>().ReverseMap();

            CreateMap<OptionItem, OptionItemModel>().ReverseMap();
            CreateMap<OptionItemAllergen, OptionItemAllergenModel>().ReverseMap();
            CreateMap<OptionItemDietary, OptionItemDietaryModel>().ReverseMap();

            // Sale
            CreateMap<Order, OrderModel>().ReverseMap();
            CreateMap<OrderItem, OrderItemModel>().ReverseMap();
            CreateMap<OrderItemDetail, OrderItemDetailModel>().ReverseMap();

        }


    }
}
