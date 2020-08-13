using Microsoft.AspNetCore.Http;

namespace Mealmate.Api
{
    public class ApiOkResponse : ApiResponse
    {
        public ApiOkResponse(string message = null) : base(StatusCodes.Status200OK, message) { }
        public ApiOkResponse(object result) : base(StatusCodes.Status200OK, result: result) { }
    }
}
