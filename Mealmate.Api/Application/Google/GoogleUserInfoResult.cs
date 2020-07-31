using Newtonsoft.Json;

namespace Mealmate.Api.Services
{
    public class GoogleUserInfoResult
    {

        [JsonProperty("iss")]
        public string Iss { get; set; }

        [JsonProperty("sub")]
        public string Sub { get; set; }

        [JsonProperty("azp")]
        public string Azp { get; set; }

        [JsonProperty("aud")]
        public string Aud { get; set; }

        [JsonProperty("iat")]
        public string Iat { get; set; }

        [JsonProperty("exp")]
        public string Exp { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("email_verified")]
        public string EmailVerified { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("picture")]
        public string Picture { get; set; }

        [JsonProperty("given_name")]
        public string GivenName { get; set; }

        [JsonProperty("family_name")]
        public string FamilyName { get; set; }

        [JsonProperty("locale")]
        public string Locale { get; set; }
    }
}