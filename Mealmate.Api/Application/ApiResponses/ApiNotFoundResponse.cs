using Microsoft.AspNetCore.Http;

using System.Net;

namespace Mealmate.Api
{
    public class ApiNotFoundResponse : ApiResponse
    {
        public ApiNotFoundResponse(string message) : base(StatusCodes.Status404NotFound, message){}

    }
}
