using Mealmate.Application.Models;
using MediatR;

namespace Mealmate.Api.Requests
{
    public class CreateRequest<T> : IRequest<T>
    {
        public T Model { get; set; }
    }
}
