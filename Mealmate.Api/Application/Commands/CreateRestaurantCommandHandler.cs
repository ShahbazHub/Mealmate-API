using System.Threading;
using System.Threading.Tasks;

using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;

using MediatR;

namespace Mealmate.Api.Application.Commands
{
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRequest<RestaurantModel>, RestaurantModel>
    {
        private readonly IRestaurantService _restaurantService;

        public CreateRestaurantCommandHandler(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        public async Task<RestaurantModel> Handle(CreateRequest<RestaurantModel> request, CancellationToken cancellationToken)
        {
            var RestaurantModel = await _restaurantService.Create(request.Model);

            return RestaurantModel;
        }
    }
}
