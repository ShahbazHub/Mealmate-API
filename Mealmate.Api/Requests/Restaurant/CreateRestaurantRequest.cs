using Mealmate.Application.Models;
using MediatR;

namespace Mealmate.Api.Requests
{
    public class CreateRestaurantRequest : IRequest<RestaurantModel>
    {
        public RestaurantModel Restaurant { get; set; }
    }
}
