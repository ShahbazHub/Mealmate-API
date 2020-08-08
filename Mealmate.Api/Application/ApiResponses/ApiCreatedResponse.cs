using Microsoft.AspNetCore.Http;

namespace Mealmate.Api
{
    public class ApiCreatedResponse : ApiResponse
    {
        public object Result { get; }
        public string Url { get; }

        public ApiCreatedResponse(object result) : base(StatusCodes.Status201Created)
        {
            Result = result;
        }
        public ApiCreatedResponse(string url, object result)
            : base(StatusCodes.Status201Created)
        {
            Url = url;
            Result = result;
        }
    }
}
