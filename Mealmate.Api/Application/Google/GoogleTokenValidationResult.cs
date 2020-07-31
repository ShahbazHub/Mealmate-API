using Newtonsoft.Json;

namespace Mealmate.Api.Services
{
    public class GoogleTokenValidationResult
    {

        [JsonProperty("issued_to")]
        public string IssuedTo { get; set; }

        [JsonProperty("audience")]
        public string Audience { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("access_type")]
        public string AccessType { get; set; }
    }
}