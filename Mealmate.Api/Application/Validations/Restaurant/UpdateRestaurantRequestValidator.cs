using Mealmate.Api.Requests;
using FluentValidation;

namespace Mealmate.Api.Application.Validations
{
    public class UpdateRestaurantRequestValidator : AbstractValidator<UpdateRestaurantRequest>
    {
        public UpdateRestaurantRequestValidator()
        {
            RuleFor(request => request.Restaurant).NotNull();
        }
    }
}
