using Mealmate.Api.Requests;
using Mealmate.Application.Interfaces;
using Mealmate.Application.Models;

using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Mealmate.Api.Application.Commands
{
    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRequest<RestaurantModel>>
    {
        private readonly IRestaurantService _restaurantService;

        public UpdateRestaurantCommandHandler(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        public async Task<Unit> Handle(UpdateRequest<RestaurantModel> request, CancellationToken cancellationToken)
        {
            await _restaurantService.Update(request.Model);

            return Unit.Value;
        }
    }
}
