using MediatR;

namespace Mealmate.Api.Requests
{
    public class DeleteRestaurantByIdRequest : IRequest
    {
        public int Id { get; set; }
    }
}
