using Mealmate.Api.Requests;
using FluentValidation;
using Mealmate.Application.Models;

namespace Mealmate.Api.Application.Validations
{
    public class CreateRestaurantRequestValidator : AbstractValidator<CreateRequest<RestaurantModel>>
    {
        public CreateRestaurantRequestValidator()
        {
            RuleFor(request => request.Model).NotNull();
        }
    }
}
