using MediatR;

namespace Mealmate.Api.Requests
{
    public class DeleteByIdRequest : IRequest
    {
        public int Id { get; set; }
    }
}
