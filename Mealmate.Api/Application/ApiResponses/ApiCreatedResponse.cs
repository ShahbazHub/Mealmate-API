using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Mealmate.Api
{
    public class ApiCreatedResponse : ApiResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; }

        public ApiCreatedResponse(object result, string url = null) : base(StatusCodes.Status201Created, result: result)
        {
            Url = url;
        }
    }
}
