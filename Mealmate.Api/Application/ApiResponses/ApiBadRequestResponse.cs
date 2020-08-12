using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mealmate.Api
{
    public class ApiBadRequestResponse : ApiResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<string> Errors { get; set; }

        public ApiBadRequestResponse(ModelStateDictionary modelState) : base(StatusCodes.Status400BadRequest)
        {
            if (modelState.IsValid)
            {
                throw new ArgumentException("ModelState must be invalid", nameof(modelState));
            }

            Errors = modelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();
        }


        public ApiBadRequestResponse(string message)
            : base(StatusCodes.Status400BadRequest, message)
        {
        }

        public ApiBadRequestResponse(IEnumerable<IdentityError> errors, string msg) 
            : base(StatusCodes.Status400BadRequest, msg)
        {
            Errors = errors.Select(x => x.Description);
        }

    }
}
