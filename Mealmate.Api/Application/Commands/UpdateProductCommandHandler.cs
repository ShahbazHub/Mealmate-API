using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Mealmate.Api.Application.Commands
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantRequest>
    {
        private readonly IRestaurantService _restaurantService;

        public UpdateRestaurantCommandHandler(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        public async Task<Unit> Handle(UpdateRestaurantRequest request, CancellationToken cancellationToken)
        {
            await _restaurantService.UpdateRestaurant(request.Restaurant);

            return Unit.Value;
        }
    }
}
