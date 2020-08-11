using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mealmate.Api
{
    public class ApiBadRequestResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }

        public ApiBadRequestResponse(ModelStateDictionary modelState): base(StatusCodes.Status400BadRequest)
        {
            if (modelState.IsValid)
            {
                throw new ArgumentException("ModelState must be invalid", nameof(modelState));
            }

            Errors = modelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();
        }

        public ApiBadRequestResponse(string error): base(StatusCodes.Status400BadRequest)
        {
            Errors.Concat(new[] { error });
        }
        public ApiBadRequestResponse(IEnumerable<IdentityError> errors): base(StatusCodes.Status400BadRequest)
        {
            Errors = errors.Select(x => x.Description);
        }

    }
}
