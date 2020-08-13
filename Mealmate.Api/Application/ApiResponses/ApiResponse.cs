using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mealmate.Api
{
    public class ApiResponse
    {
        public int StatusCode { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object Result { get; }
      
        public ApiResponse(int statusCode, string message = null, object result = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
            Result = result;
        }
        private static string GetDefaultMessageForStatusCode(int statusCode)
        {
            return ((HttpStatusCode)statusCode).ToString();

        }
    }

}
