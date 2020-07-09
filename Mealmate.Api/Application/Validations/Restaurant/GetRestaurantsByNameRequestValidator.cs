using Mealmate.Api.Requests;
using FluentValidation;

namespace Mealmate.Api.Application.Validations
{
    public class GetRestaurantsByNameRequestValidator : AbstractValidator<GetResturantsByNameRequest>
    {
        public GetRestaurantsByNameRequestValidator()
        {
            RuleFor(request => request.Name).NotNull();
        }
    }
}
