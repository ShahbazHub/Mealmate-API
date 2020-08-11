using Microsoft.AspNetCore.Http;

namespace Mealmate.Api
{
    public class ApiUnAuthorizedResponse : ApiResponse
    {
        public ApiUnAuthorizedResponse(string Message) : base(StatusCodes.Status401Unauthorized, Message){}
    }
}
