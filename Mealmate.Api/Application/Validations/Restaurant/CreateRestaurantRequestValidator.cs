using Mealmate.Api.Requests;
using FluentValidation;

namespace Mealmate.Api.Application.Validations
{
    public class CreateRestaurantRequestValidator : AbstractValidator<CreateRestaurantRequest>
    {
        public CreateRestaurantRequestValidator()
        {
            RuleFor(request => request.Restaurant).NotNull();
        }
    }
}
