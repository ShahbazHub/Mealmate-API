using Mealmate.Api.Requests;
using FluentValidation;
using Mealmate.Application.Models;

namespace Mealmate.Api.Application.Validations
{
    public class UpdateRestaurantRequestValidator : AbstractValidator<UpdateRequest<RestaurantModel>>
    {
        public UpdateRestaurantRequestValidator()
        {
            RuleFor(request => request.Model).NotNull();
        }
    }
}
