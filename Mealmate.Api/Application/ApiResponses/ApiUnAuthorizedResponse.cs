using Microsoft.AspNetCore.Http;

namespace Mealmate.Api
{
    public class ApiUnAuthorizedResponse : ApiResponse
    {
        public ApiUnAuthorizedResponse(string message) : base(StatusCodes.Status401Unauthorized, message) {}
    }
}
