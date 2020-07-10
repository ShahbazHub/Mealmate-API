using System.Threading;
using System.Threading.Tasks;

using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;

using MediatR;

namespace Mealmate.Api.Application.Commands
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantRequest, RestaurantModel>
    {
        private readonly IRestaurantService _restaurantService;

        public CreateRestaurantCommandHandler(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        public async Task<RestaurantModel> Handle(CreateRestaurantRequest request, CancellationToken cancellationToken)
        {
            var RestaurantModel = await _restaurantService.Create(request.Restaurant);

            return RestaurantModel;
        }
    }
}
