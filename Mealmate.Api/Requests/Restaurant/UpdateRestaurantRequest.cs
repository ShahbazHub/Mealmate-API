using Mealmate.Application.Models;
using MediatR;

namespace Mealmate.Api.Requests
{
    public class UpdateRestaurantRequest : IRequest
    {
        public RestaurantModel Restaurant { get; set; }
    }
}
