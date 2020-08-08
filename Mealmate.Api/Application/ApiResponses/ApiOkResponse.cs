using Microsoft.AspNetCore.Http;

using System.Net;

namespace Mealmate.Api
{
    public class ApiOkResponse : ApiResponse
    {
        public object Result { get; }

        public ApiOkResponse(object result): base(StatusCodes.Status200OK)
        {
            Result = result;
        }
    }
}
