using Mealmate.Api.Requests;
using FluentValidation;

namespace Mealmate.Api.Application.Validations
{
    public class DeleteRestaurantRequestValidator : AbstractValidator<DeleteByIdRequest>
    {
        public DeleteRestaurantRequestValidator()
        {
            RuleFor(request => request.Id).GreaterThan(0);
        }
    }
}
