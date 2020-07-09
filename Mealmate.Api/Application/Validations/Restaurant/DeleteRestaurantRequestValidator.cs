using Mealmate.Api.Requests;
using FluentValidation;

namespace Mealmate.Api.Application.Validations
{
    public class DeleteRestaurantRequestValidator : AbstractValidator<DeleteRestaurantByIdRequest>
    {
        public DeleteRestaurantRequestValidator()
        {
            RuleFor(request => request.Id).GreaterThan(0);
        }
    }
}
