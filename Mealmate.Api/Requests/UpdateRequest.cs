using Mealmate.Application.Models;
using MediatR;

namespace Mealmate.Api.Requests
{
    public class UpdateRequest<T> : IRequest
    {
        public T Model { get; set; }
    }
}
